using AkerTeklif.Data;
using AkerTeklif.Features.Categories.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AkerTeklif.Features.Categories
{
    public class CategoryService(AppDbContext context)
    {
        public async Task CreateCategoryHandler(CreateCategoryDTO categoryDto)
        {
            Category category = new Category
            {
                Name = categoryDto.Name
            };
            await context.AddAsync(category);
            await context.SaveChangesAsync();
        }

        public async Task<List<GetCategoryDTO>> GetCategoryHandler()
        {
            var categories = await context.Categories.ToListAsync();
            var categoriesDTO = categories.Select(x => new GetCategoryDTO
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return categoriesDTO;
        }

        public async Task UpdateCategoryHandler(UpdateCategoryDTO updateCategoryDTO)
        {
            var category = new Category
            {
                Name = updateCategoryDTO.Name,
                Id = updateCategoryDTO.Id
            };

            context.Update(category);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCategoryHandler(int id)
        {
            var category = await context.Categories.FindAsync(id);
            context.Remove(category!);
            await context.SaveChangesAsync();
        }
    }
}
