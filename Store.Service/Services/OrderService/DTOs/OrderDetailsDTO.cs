using Store.Data.Entities.OrderEntities;
using Store.Data.Entities;

namespace Store.Service.Services.OrderService.DTOs
{
    public class OrderDetailsDTO
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public AddressDTO ShippingAddress { get; set; }
        public string DeliveryMethodName { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public OrderPaymentStatus OrderPaymentStatus { get; set; }
        public int? DeliveryMethodId { get; set; }
        public IReadOnlyList<OrderItemDTO> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingPrice { get; set; }  
        public decimal Total { get; set; }
        public string? BasketId { get; set; }
        public string? PaymentIntentId { get; set; }
    }
}
