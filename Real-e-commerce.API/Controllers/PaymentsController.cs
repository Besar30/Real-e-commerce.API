using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Real_e_commerce.API.Extentions;
using Real_e_commerce.API.SignalR;
using Real_e_commerce.Core.Entities.OrderAggregate;
using Real_e_commerce.Core.Interfaces;
using Real_e_commerce.Core.Specifiactions;
using Real_e_commerce.Infrastructure.Setting;
using Stripe;

namespace Real_e_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(IPaymentServices paymentServices
        ,IUnitOfWork unitOfWork ,ILogger<PaymentsController> logger,IOptions<StripeSetting> options,
        IHubContext<NotificationHub> hubContext): ControllerBase
    {
        private readonly string _whSecret = options.Value.WhSecret;
        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<IActionResult>CreateOrUpdatePaymentIntent([FromRoute]string cartId)
        {
            var cart=await paymentServices.CreateOrUpdatePaymentIntent(cartId);
            if (cart == null) return BadRequest("problem with your cart");
            return Ok(cart);
        }
        [HttpGet("delivery-methods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            return Ok(await unitOfWork.DeliveryMethodRepository.GetAll().ToListAsync());
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json=await new StreamReader(Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent=ConstructStripeEvent(json);
                if(stripeEvent.Data.Object is not PaymentIntent intent)
                {
                    return BadRequest("Invalid event Data");
                }
                await HandlePaymentIntentSucceeded(intent);
                return Ok();
            }
            catch (StripeException ex)
            {
                logger.LogError(ex, "Stripe webhook error");
                return StatusCode(StatusCodes.Status500InternalServerError, "Stripe webhook error");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unexpected error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        private async Task HandlePaymentIntentSucceeded(PaymentIntent intent)
        {
            if (intent.Status == "succeeded")
            {
                var spec = new OrderSpecification(intent.Id, true);
                var order=await unitOfWork.OrderRepositoty.GetEntityWithSpec(spec)
                    ??throw new Exception("Order Not Found");
                if ((long)order.GetTotal() * 100 != intent.Amount)
                {
                    order.Status = OrderStatus.PaymentMismatch;
                }
                else
                {
                    order.Status = OrderStatus.PaymentRecived;
                }
                await unitOfWork.Save();
                //TDO:SignalR
                var connectionId = NotificationHub.GetConnectionIdByEmail(order.BuyerEmail);
                if (!string.IsNullOrEmpty(connectionId))
                {
                    await hubContext.Clients.Client(connectionId).SendAsync("OrderCompleteNotification", order.ToDto());
                }
            }
        }

        private Event ConstructStripeEvent(string json)
        {
            try
            {
                return EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"]
                    , _whSecret, throwOnApiVersionMismatch: false);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Failed to construct stripe event");
                throw new StripeException("Invalid signature");
            }
        }
    }
}
