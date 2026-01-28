namespace AkerTeklif.Features.Users.DTOs
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string UserEmail { get; set; }
        public required string UserPassword { get; set; }
        public required string ConfirmPassword { get; set; }
        public required string RoleName { get; set; }
    }
}
