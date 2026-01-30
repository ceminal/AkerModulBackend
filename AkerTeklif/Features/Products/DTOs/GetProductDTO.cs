namespace AkerTeklif.Features.Products.DTOs
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
        public string? Details { get; set; }
        public decimal Price { get; set; }
    }
}
