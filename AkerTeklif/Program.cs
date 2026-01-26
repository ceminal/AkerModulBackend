using AkerTeklif.Data;
using AkerTeklif.Extensions;
using AkerTeklif.Features.Categories;
using AkerTeklif.Features.Products;
using AkerTeklif.Features.Users;
using AkerTeklif.Features.Users.Login;
using AkerTeklif.Features.Users.Register;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddServices();
builder.Services.AddJWTService(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(x => x.Theme = ScalarTheme.BluePlanet);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
