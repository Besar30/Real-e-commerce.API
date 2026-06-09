using Real_e_commerce.Core.Entities;

namespace Real_e_commerce.Core.Specifiactions
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(string? Brand, string? Type,string? sort) : base(x =>
           (string.IsNullOrEmpty(Brand) || x.Brand == Brand) &&
           (string.IsNullOrEmpty(Type)) || x.Type == Type
        )
        {
            switch (sort)
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
