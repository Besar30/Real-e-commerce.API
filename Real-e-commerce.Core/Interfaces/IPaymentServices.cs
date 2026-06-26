using Real_e_commerce.Core.Entities;
namespace Real_e_commerce.Core.Interfaces
{
    public interface IPaymentServices
    {
        Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId);
    }
}
