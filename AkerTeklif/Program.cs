using AkerTeklif.Data;
using AkerTeklif.Extensions;
using AkerTeklif.Features.Categories;
using AkerTeklif.Features.Products;
using AkerTeklif.Features.Users;
using AkerTeklif.Features.Users.Login;
using AkerTeklif.Features.Users.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowReactApp",
//        policy =>
//        {
//            policy.SetIsOriginAllowed(origin => true)
//                  .AllowAnyHeader()
//                  .AllowAnyMethod()
//                  .AllowCredentials(); 
//        });
//});
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", opts =>
    {

        opts.AllowAnyHeader();
        opts.AllowAnyMethod();
        opts.WithOrigins(allowedOrigins!);
    });

});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddServices();
builder.Services.AddJWTService(builder.Configuration);
var app = builder.Build();
app.UseHttpsRedirection();
// CORS
app.UseCors("AllowReactApp");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(x => x.Theme = ScalarTheme.BluePlanet);
}



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        Console.WriteLine("Veritabaný baþarýyla migrate edildi.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration sýrasýnda hata: {ex.Message}");
    }
}

app.Run();
