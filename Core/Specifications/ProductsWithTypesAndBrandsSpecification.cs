using Core.Entities;

namespace Core.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification(
        ProductSpecParams productParams)
        : base(
            x =>
                ( string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower()
                    .Contains(productParams.Search) ) &&
                ( !productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId )
                && ( !productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId ))
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
        AddOrderBy(x => x.Name);

        ApplyPaging(
            productParams.PageSize * ( productParams.PageNumber - 1 ),
            productParams.PageSize);

        if ( !string.IsNullOrEmpty(productParams.Sort) )
        {
            switch ( productParams.Sort )
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);

                    break;

                case "priceDesc":
                    AddOrderByDescending(p => p.Price);

                    break;

                default:
                    AddOrderBy(p => p.Name);

                    break;
            }
        }
    }

    public ProductsWithTypesAndBrandsSpecification(
        int id)
        : base(x => x.Id == id)
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
    }
}
