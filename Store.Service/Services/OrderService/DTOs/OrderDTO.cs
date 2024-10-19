using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService.DTOs
{
    public class OrderDTO
    {
        public string BasketId { get; set; }
        public string BuyerEmail { get; set; }

        [Required]
        public int DeliveryMethodId { get; set; }
        public AddressDTO ShippingAddress { get; set; }
    }
}
