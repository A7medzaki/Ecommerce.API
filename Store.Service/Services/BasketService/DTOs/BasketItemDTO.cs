using System.ComponentModel.DataAnnotations;

namespace Store.Service.Services.BasketService.DTOs
{
    public class BasketItemDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Price Must Be Greater Than Zero")]
        [Required]
        public decimal Price { get; set; }

        [Range(1, 10, ErrorMessage = "Quantity Must Be From 1 TO 10")]
        [Required]
        public int Quantity { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string BrandName { get; set; }

        [Required]
        public string TypeName { get; set; }

    }
}