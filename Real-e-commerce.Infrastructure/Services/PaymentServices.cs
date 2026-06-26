using Microsoft.Extensions.Options;
using Real_e_commerce.Core.Entities;
using Real_e_commerce.Core.Interfaces;
using Real_e_commerce.Infrastructure.Setting;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Infrastructure.Services
{
    public class PaymentServices(IOptions<StripeSetting> options,ICartServices cartServices,IUnitOfWork unitOfWork): IPaymentServices
    {
        private readonly StripeSetting options = options.Value;

        public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId)
        {
            StripeConfiguration.ApiKey = options.SecretKey;
            ShoppingCart cart=await cartServices.GetCartAsync(cartId);
            if (cart == null) return null;
            decimal shippingPrice = 0m;
            if (cart.DeliveryMethodId.HasValue)
            {
                DeliveryMethod deliveryMethod = await unitOfWork.DeliveryMethodRepository.GetById((int)cart.DeliveryMethodId);
                if (deliveryMethod == null) return null;
                shippingPrice = deliveryMethod.Price;
            }
            foreach (var item in cart.Items) { 
                var productItem=await unitOfWork.ProductRepository.GetById(item.ProductId);
                if (productItem == null) return null;
                if (item.Price !=productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }
            var service = new PaymentIntentService();
            PaymentIntent? intent = null;
            if (string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions
                {
                    Amount = (long)cart.Items.Sum(x => (x.Quantity * (x.Price * 100))) + (long)(shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };
                intent=await service.CreateAsync(option);
                cart.PaymentIntentId = intent.Id;
                cart.ClientSecret= intent.ClientSecret;
               
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)cart.Items.Sum(x => (x.Quantity * (x.Price * 100))) + (long)(shippingPrice * 100)
                };
                intent=await service.UpdateAsync(cart.PaymentIntentId, options);
            }
            await cartServices.SetCartAsync(cart);
            return cart;
        }
    }
}
