

namespace ecomm_nike.Models
{
    public class Size
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ProductId { get; set; }

        public Product? Product { get; set; }
        public string SizeOfShoe { get; set; } = string.Empty;

        public string Stock { get; set; } = string.Empty;
    }
}