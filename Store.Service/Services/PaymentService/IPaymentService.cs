using Store.Service.Services.BasketService.DTOs;
using Store.Service.Services.OrderService.DTOs;

namespace Store.Service.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<CustomerBasketDTO> CreateOrUpdateIntentPayment(CustomerBasketDTO input);
        Task<OrderDetailsDTO> UpdateOrderPaymentSucceeded(string paymentIntentId);
        Task<OrderDetailsDTO> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
