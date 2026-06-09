using Real_e_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Core.Dtos.ProductFeatuer
{
    public static class ProductQueries
    {
        public static void UpdateEntity(this UpdateProductDto dto,Product product)
        {
            product.Name=dto.Name;
            product.Description=dto.Description;
            product.Price=dto.Price;
            product.PictureUrl=dto.PictureUrl;
            product.Brand=dto.Brand;
            product.Type=dto.Type;
            product.QuantityInStock=dto.QuantityInStock;
        }
    }
}
