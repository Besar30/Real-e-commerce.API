using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Core.Dtos.ProductFeatuer
{
    public record UpdateProductDto
    {
        public required string Name { get; init; }
        public required string Description { get; init; }
        public decimal Price { get; init; }
        public required string PictureUrl { get; init; }
        public required string Type { get; init; }
        public required string Brand { get; init; }
        public int QuantityInStock { get; init; }
    }
}
