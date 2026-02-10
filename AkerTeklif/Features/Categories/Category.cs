using AkerTeklif.Features.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AkerTeklif.Features.Categories
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        [ForeignKey("ParentCategoryId")]
        public Category? ParentCategory { get; set; }
        public List<Category> SubCategories { get; set; } = new();
        public List<Product>? Products { get; set; }
    }
}