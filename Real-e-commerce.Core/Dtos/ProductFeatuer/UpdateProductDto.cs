using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Core.Dtos.ProductFeatuer
{
    public record UpdateProductDto
    {
        [Required]
        public  string Name { get; init; } = string.Empty;
        [Required]
        public  string Description { get; init; }=string.Empty;
        [Range(.01,double.MaxValue,ErrorMessage ="price Must be greather than 0")]
        public decimal Price { get; init; }
        [Required]
        public  string PictureUrl { get; init; } = string.Empty;
        [Required]
        public  string Type { get; init; } = string.Empty;
        [Required]
        public  string Brand { get; init; } = string.Empty;
        [Range(1,int.MaxValue,ErrorMessage ="Quantity in stock must be at least 1")]
        public int QuantityInStock { get; init; }
    }
}
