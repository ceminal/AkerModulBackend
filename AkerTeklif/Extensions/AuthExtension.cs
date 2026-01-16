using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AkerTeklif.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddJWTService(this IServiceCollection services, IConfiguration configuration)
        {
            // 2. Authentication Servisini Ekleyelim
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                    ClockSkew = TimeSpan.Zero // Token süresi bittiği an geçersiz olsun (Varsayılan 5 dk tolerans vardır)
                };
            });
            return services;
        }
    }
}
