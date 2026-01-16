using AkerTeklif.Data;
using AkerTeklif.Features.Categories;
using AkerTeklif.Features.Jwt;
using AkerTeklif.Features.Products;
using AkerTeklif.Features.Users;
using AkerTeklif.Features.Users.Login;
using AkerTeklif.Features.Users.Register;
using Microsoft.AspNetCore.Identity;

namespace AkerTeklif.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {
            services.AddDbContext<AppDbContext>();
            services.AddIdentity<AppUser, IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = false;           // Rakam zorunluluğunu kaldırır (0-9)
                options.Password.RequireLowercase = false;       // Küçük harf zorunluluğunu kaldırır
                options.Password.RequireUppercase = false;       // Büyük harf zorunluluğunu kaldırır
                options.Password.RequireNonAlphanumeric = false; // Özel karakter (!, @, # vb.) zorunluluğunu kaldırır
                options.Password.RequiredLength = 3;             // Minimum şifre uzunluğunu belirler (Varsayılan 6'dır)
                options.Password.RequiredUniqueChars = 0;        // Tekrar etmemesi gereken karakter sayısını sıfırlar
            }).AddEntityFrameworkStores<AppDbContext>();
            services.AddScoped<CategoryService>();
            services.AddScoped<ProductService>();
            services.AddScoped<RegisterService>();
            services.AddScoped<LoginService>();
            services.AddScoped<TokenService>();
            return services;
        }

    }
}
