using System.ComponentModel.DataAnnotations.Schema;

namespace AkerTeklif.Features.Jwt
{
    public class TokenResponse
    {
        public string? AccessToken { get; set; }
        [NotMapped]
        public DateTime Expires { get; set; }
        
    }
}
