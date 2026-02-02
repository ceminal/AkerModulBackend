namespace AkerTeklif.Features.Products.DTOs
{
    public class CreateProductDTO
    {
        public required string Name { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
        public string? Details { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
