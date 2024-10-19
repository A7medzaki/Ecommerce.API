namespace Store.Service.Services.BasketService.DTOs
{
    public class CustomerBasketDTO
    {
        public string? Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItemDTO> BasketItems { get; set; } = new List<BasketItemDTO>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
