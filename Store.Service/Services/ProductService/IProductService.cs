using Store.Repository.Specifications.ProductSpecifications;
using Store.Service.Helper;
using Store.Service.Services.ProductService.DTOs;
namespace Store.Service.Services.ProductService
{
    public interface IProductService
    {
        Task<ProductDetailsDto> GetProductByIdAsync(int? productId);
        Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductAsync(ProductSpecifications specs);
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypeAsync();
    }
}
