using AkerTeklif.Features.Categories;

namespace AkerTeklif.Features.Products
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Image {  get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string? Details { get; set; }
        public Category? Category { get; set; }
        public int Stock { get; set; }
    }
}
