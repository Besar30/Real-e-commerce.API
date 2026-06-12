using Real_e_commerce.Core.Entities;

namespace Real_e_commerce.Core.Specifiactions
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecificationPrams specParams) : base(
            x=>
            (specParams.Brands.Count==0 || specParams.Brands.Contains(x.Brand))&&
            (specParams.Types.Count==0||specParams.Types.Contains(x.Type))
        )
        {
            ApplyPagination((specParams.pageIndex - 1) * specParams.PageSize, specParams.PageSize);
            switch (specParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(x => x.Price); break;
                case "priceDesc":
                    AddOrderByDescending(x => x.Price); break;
                default:
                    AddOrderBy(x => x.Name);
                    break;
            }
        }
    }
}
