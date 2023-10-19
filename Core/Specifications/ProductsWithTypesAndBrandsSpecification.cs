using Core.Entities;

namespace Core.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification(
        string sort)
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
        AddOrderBy(x => x.Name);

        if ( !string.IsNullOrEmpty(sort) )
        {
            switch ( sort )
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
