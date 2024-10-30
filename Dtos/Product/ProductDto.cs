using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecomm_nike.Models;

namespace ecomm_nike.Dtos.Product
{
    public class ProductDto
    {
        
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;

        public string Src { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public int Price { get; set; }

        public int OriginalPrice { get; set; }

        public string SpecialMention { get; set; } = string.Empty;
    }
}