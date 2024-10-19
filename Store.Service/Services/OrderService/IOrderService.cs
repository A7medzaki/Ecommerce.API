using Store.Data.Entities;
using Store.Service.Services.OrderService.DTOs;

namespace Store.Service.Services.OrderService
{
    public interface IOrderService
    {
        Task<OrderDetailsDTO> CreateOrderAsync(OrderDTO input);
        Task<IReadOnlyList<OrderDetailsDTO>> GetAllOrdersForUserAsync(string buyerEmail);
        Task<OrderDetailsDTO> GetOrderByIdAsync(Guid id);
        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync(OrderDTO input);
    }
}
