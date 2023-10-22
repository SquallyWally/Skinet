using Core.Entities;

namespace Core.Specifications;

public class ProductWithFiltersForCountSpecifications : BaseSpecification<Product>
{
    public ProductWithFiltersForCountSpecifications(
        ProductSpecParams productParams)
        : base(
            x => ( !productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId )
                && ( !productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId ))
    {
    }
}
