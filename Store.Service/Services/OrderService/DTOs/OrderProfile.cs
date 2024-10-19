using AutoMapper;
using Store.Data.Entities.OrderEntities;

namespace Store.Service.Services.OrderService.DTOs
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress, AddressDTO>().ReverseMap();

            CreateMap<Order, OrderDetailsDTO>()
                .ForMember(dest => dest.DeliveryMethodName, options => options.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice, options => options.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductItemId, options => options.MapFrom(src => src.ProductItem.ProductItemId))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.ProductItem.ProductName))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemPictureUrlResolver>()).ReverseMap();
        }
    }
}
