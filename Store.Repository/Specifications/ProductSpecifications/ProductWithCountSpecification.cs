using Store.Data.Entities;

namespace Store.Repository.Specifications.ProductSpecifications
{
    public class ProductWithCountSpecification : BaseSpecification<Product>
    {
        public ProductWithCountSpecification(ProductSpecifications specs)
                : base(product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value) &&
                      (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value) &&
                      (string.IsNullOrEmpty(specs.Search) || product.Name.Trim().ToLower().Contains(specs.Search))
                )
        {

        }

    }
}
