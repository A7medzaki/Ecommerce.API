using AutoMapper;
using Store.Repository.Basket;
using Store.Repository.Basket.Models;
using Store.Service.Services.BasketService.DTOs;

namespace Store.Service.Services.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
            => await _basketRepository.DeleteBasketAsync(basketId);

        public async Task<CustomerBasketDTO> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);

            if(basket is null)
                return new CustomerBasketDTO();

            var mappedBasket = _mapper.Map<CustomerBasketDTO>(basket);

            return mappedBasket;
        }

        public async Task<CustomerBasketDTO> UpdateBasketAsync(CustomerBasketDTO input)
        {
            if (input.Id is null)
                input.Id = GenerateRandomBasketId();

            var customerBasket = _mapper.Map<CustomerBasket>(input);

            var updatedBasket = await _basketRepository.UpdateBasketAsync(customerBasket);

            var mappedUpdatedBasket = _mapper.Map<CustomerBasketDTO>(updatedBasket);

            return mappedUpdatedBasket;
        }

        private string GenerateRandomBasketId()
        {
            Random random = new Random();

            int randomDigits = random.Next(1000, 10000);

            return $"BS-{randomDigits}";
        }
    }
}
