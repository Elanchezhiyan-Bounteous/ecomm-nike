using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomm_nike.Models
{
    public class Image
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }

        public Product? Product { get; set; }
        public string Alt { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}