using AkerTeklif.Data;
using AkerTeklif.Features.Categories;
using AkerTeklif.Features.Products;
using AkerTeklif.Features.Users;
using AkerTeklif.Features.Users.Login;
using AkerTeklif.Features.Users.Register;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddIdentity<AppUser, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = false;           // Rakam zorunluluðunu kaldýrýr (0-9)
    options.Password.RequireLowercase = false;       // Küçük harf zorunluluðunu kaldýrýr
    options.Password.RequireUppercase = false;       // Büyük harf zorunluluðunu kaldýrýr
    options.Password.RequireNonAlphanumeric = false; // Özel karakter (!, @, # vb.) zorunluluðunu kaldýrýr
    options.Password.RequiredLength = 3;             // Minimum þifre uzunluðunu belirler (Varsayýlan 6'dýr)
    options.Password.RequiredUniqueChars = 0;        // Tekrar etmemesi gereken karakter sayýsýný sýfýrlar
}).AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<RegisterService>();
builder.Services.AddScoped<LoginService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(x => x.Theme = ScalarTheme.BluePlanet);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
