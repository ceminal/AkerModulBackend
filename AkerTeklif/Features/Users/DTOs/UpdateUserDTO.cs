namespace AkerTeklif.Features.Users.DTOs
{
    public class UpdateUserDTO
    {
        public required int Id { get; set; }
        public required string UserName { get; set; }
        public required string UserEmail { get; set; }
        //public string? UserPassword { get; set; }
        //public string? ConfirmPassword { get; set; }
        public required string Role { get; set; }
    }
}