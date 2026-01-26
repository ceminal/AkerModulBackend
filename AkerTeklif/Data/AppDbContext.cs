using AkerTeklif.Features.Categories;
using AkerTeklif.Features.Products;
using AkerTeklif.Features.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AkerTeklif.Data
{
    public class AppDbContext(IConfiguration configuration) : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("sqlserver"));
            }
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
