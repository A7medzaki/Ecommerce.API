using AutoMapper;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Interfaces;
using Store.Repository.Specifications.OrderSpecifications;
using Store.Service.Services.BasketService;
using Store.Service.Services.OrderService.DTOs;
using Store.Service.Services.PaymentService;
using Product = Store.Data.Entities.Product;

namespace Store.Service.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService
            (IBasketService basketService, IUnitOfWork unitOfWork, IMapper mapper, IPaymentService paymentService)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentService = paymentService;
        }
        public async Task<OrderDetailsDTO> CreateOrderAsync(OrderDTO input)
        {
            // Get Basket
            var basket = await _basketService.GetBasketAsync(input.BasketId);

            if (basket is null)
                throw new Exception("Basket Not Exist");


            // fill order Items in the basket
            var orderItems = new List<OrderItemDTO>();

            foreach (var basketItem in basket.BasketItems)
            {
                var productItem = await _unitOfWork.Repository<Product, int>().GetByIdAsync(basketItem.ProductId);

                if (productItem is null)
                    throw new Exception($"Product With ID : {basketItem.ProductId} Not Exist");

                var ItemOrdered = new ProductItem
                {
                    PictureUrl = productItem.PictureUrl,
                    ProductItemId = productItem.Id,
                    ProductName = productItem.Name,
                };

                var orderItem = new OrderItem
                {
                    Price = productItem.Price,
                    Quantity = basketItem.Quantity,
                    ProductItem = ItemOrdered
                };

                var mappedOrderItem = _mapper.Map<OrderItemDTO>(orderItem);

                orderItems.Add(mappedOrderItem);
            }


            // Get Delivery Method
            var deliveryMethod = _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId);

            if (deliveryMethod is null)
                throw new Exception("Delivery Method Not Provided");


            // Calculate SubTotal
            var subTotal = orderItems.Sum(item => item.Quantity * item.Price);


            //payments
            var specs = new OrderWithPaymentIntentSpecification(basket.PaymentIntentId);

            var existingOrder = await _unitOfWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);

            if (existingOrder is null)
                await _paymentService.CreateOrUpdateIntentPayment(basket);


            //create order
            var mappedShippingAddress = _mapper.Map<ShippingAddress>(input.ShippingAddress);

            var mappedOrderItems = _mapper.Map<List<OrderItem>>(orderItems);

            var order = new Order
            {
                DeliveryMethodId = deliveryMethod.Id,
                ShippingAddress = mappedShippingAddress,
                BuyerEmail = input.BuyerEmail,
                BasketId = input.BasketId,
                OrderItems = mappedOrderItems,
                SubTotal = subTotal,
                PaymentIntentId = basket.PaymentIntentId
            };

            await _unitOfWork.Repository<Order, Guid>().AddAsync(order);

            await _unitOfWork.CompleteAsync();

            var mappedOrder = _mapper.Map<OrderDetailsDTO>(order);

            return mappedOrder;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync(OrderDTO input)
            => await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();

        public async Task<IReadOnlyList<OrderDetailsDTO>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            var specs = new OrderWithItemSpecification(buyerEmail);

            var orders = await _unitOfWork.Repository<Order, Guid>().GetAllWithSpecificationAsync(specs);

            if (!orders.Any())
                throw new Exception("You Do Not Have Any Orders Yet!");

            var mappedOrder = _mapper.Map<List<OrderDetailsDTO>>(orders); 
            
            return mappedOrder;
        }

        public async Task<OrderDetailsDTO> GetOrderByIdAsync(Guid id)
        {
            var specs = new OrderWithItemSpecification(id);

            var order = await _unitOfWork.Repository<Order, Guid>().GetAllWithSpecificationAsync(specs);

            if (order is null)
                throw new Exception($"There Is No Order With ID: {id}");

            var mappedOrder = _mapper.Map<OrderDetailsDTO>(order);

            return mappedOrder;
        }
    }
}
