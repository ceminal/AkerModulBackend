using AkerTeklif.Features.Products;

namespace AkerTeklif.Features.Categories
{
    public class Category
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public List<Product>? Products { get; set; }
    }
}
