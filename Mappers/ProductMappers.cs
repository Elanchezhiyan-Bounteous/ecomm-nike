using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomm_nike.Dtos.Product;
using ecomm_nike.Models;

namespace ecomm_nike.Mappers
{
    public static class ProductMappers
    {
        public static ProductDto ToProductDto(this Product productModel)
        {
            return new ProductDto
            {
                Id = productModel.Id,
                Category = productModel.Category,
                Desc = productModel.Desc,
                Name = productModel.Name,
                OriginalPrice = productModel.OriginalPrice,
                Price = productModel.Price,
                SpecialMention = productModel.SpecialMention,
                Src = productModel.Src,


            };
        }

        public static Product ToProductFromCreateProductDto(this CreateProductRequestDto productDto)
        {
            return new Product
            {
                Category = productDto.Category,
                Name = productDto.Name,
                Desc = productDto.Desc,
                OriginalPrice = productDto.OriginalPrice,
                Price = productDto.Price,
                SpecialMention = productDto.SpecialMention,
                Src = productDto.Src,

            };
        }

    }
}