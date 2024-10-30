using ecomm_nike.Models;
using Microsoft.EntityFrameworkCore;

namespace ecomm_nike
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Image> Images { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Size> Sizes { get; set; }




    }
}