
namespace ecomm_nike.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;

        public string Src { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public int Price { get; set; }

        public int OriginalPrice { get; set; }

        public string SpecialMention { get; set; } = string.Empty;

        public List<Image> Images { get; set; } = [];
        public List<Color> Colors { get; set; } = [];

        public List<Size> Sizes { get; set; } = [];

        public List<Review> Reviews {get; set;} = [];


    }
}