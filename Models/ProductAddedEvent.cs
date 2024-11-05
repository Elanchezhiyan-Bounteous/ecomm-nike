using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomm_nike.Models
{
    public class ProductAddedEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public DateTime EntryTime { get; set; } = new DateTime().ToUniversalTime();

    }
}