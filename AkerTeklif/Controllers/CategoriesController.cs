using AkerTeklif.Features.Categories;
using AkerTeklif.Features.Categories.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AkerTeklif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController(CategoryService categoryService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO categoryDTO)
        {
            await categoryService.CreateCategoryHandler(categoryDTO);
            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {
            var categories = await categoryService.GetCategoryHandler();
            return Ok(categories);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            await categoryService.UpdateCategoryHandler(updateCategoryDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await categoryService.DeleteCategoryHandler(id);
            return Ok();
        }
    }
}
