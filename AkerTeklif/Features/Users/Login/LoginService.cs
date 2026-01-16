using AkerTeklif.Features.Jwt;
using Microsoft.AspNetCore.Identity;

namespace AkerTeklif.Features.Users.Login
{
    public class LoginService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, TokenService tokenService)
    {
        public async Task<TokenResponse> Login(LoginDTO loginDTO)
        {
            var user = await userManager.FindByNameAsync(loginDTO.UserName);
            var result = await signInManager.CheckPasswordSignInAsync(user!, loginDTO.Password, false);

            if (result.Succeeded)
            {
                var token = tokenService.GenerateToken(user!);
                return token;

            }
            else
            {
                return new TokenResponse();
            }
        }
    }
}
