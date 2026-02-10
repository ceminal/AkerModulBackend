using AkerTeklif.Features.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace AkerTeklif.Features.Categories.DTOs
{
    public class GetCategoryDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
