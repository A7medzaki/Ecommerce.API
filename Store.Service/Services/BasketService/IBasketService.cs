using Store.Repository.Basket.Models;
using Store.Service.Services.BasketService.DTOs;

namespace Store.Service.Services.BasketService
{
    public interface IBasketService
    {
        Task<CustomerBasketDTO> GetBasketAsync(string basketId);
        Task<CustomerBasketDTO> UpdateBasketAsync(CustomerBasketDTO basket);
        Task<bool> DeleteBasketAsync(string basketId);

    }
}
