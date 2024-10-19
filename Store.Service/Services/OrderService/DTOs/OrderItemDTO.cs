using Store.Data.Entities.OrderEntities;

namespace Store.Service.Services.OrderService.DTOs
{
    public class OrderItemDTO
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public Guid OrderId { get; set; }

    }
}
