namespace AkerTeklif.Features.Jwt
{
    public class TokenResponse
    {
        public string? AccessToken { get; set; }
        public DateTime Expires { get; set; }
        
    }
}
