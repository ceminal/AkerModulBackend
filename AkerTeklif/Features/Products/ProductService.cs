using AkerTeklif.Data;
using AkerTeklif.Features.Categories.DTOs;
using AkerTeklif.Features.Products.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AkerTeklif.Features.Products
{
    public class ProductService(AppDbContext context)
    {
        public async Task CreateProductHandler(CreateProductDTO productDTO)
        {
            Product product = new Product
            {
                CategoryId = productDTO.CategoryId,
                Name = productDTO.Name,
                Image = productDTO.Image,
                Price = productDTO.Price,
                Details = productDTO.Details,
            };

            await context.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public async Task<List<GetProductDTO>> GetProductHandler()
        {
            var products = await context.Products.ToListAsync();
            var productsDTO = products.Select(x => new GetProductDTO
            {
                CategoryId = x.CategoryId,
                Name = x.Name,
                Image = x.Image,
                Price = x.Price,
                Details = x.Details,
                Id = x.Id

            }).ToList();
            return productsDTO;
        }

        public async Task DeleteProductHandler(int id)
        {
            var product = await context.Products.FindAsync(id);
            context.Products.Remove(product!);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProductHandler(UpdateProductDTO updateProductDTO)
        {
            var product = new Product
            {
                CategoryId = updateProductDTO.CategoryId,
                Name = updateProductDTO.Name,
                Image = updateProductDTO.Image,
                Price = updateProductDTO.Price,
                Details = updateProductDTO.Details,
                Id = updateProductDTO.Id
            };

            context.Update(product);
            await context.SaveChangesAsync();
        }
    }
}
