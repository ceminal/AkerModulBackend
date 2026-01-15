using Microsoft.AspNetCore.Identity;

namespace AkerTeklif.Features.Users.Login
{
    public class LoginService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        public async Task<LoginResponse> Login(LoginDTO loginDTO)
        {
            var user = await userManager.FindByNameAsync(loginDTO.UserName);
            var result = await signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (result.Succeeded)
            {
                return new LoginResponse
                {
                    Message = "Giriş başarılı"
                };
            }
            else
            {
                return new LoginResponse
                {
                    Message = "Giriş Başarısız"
                };
            }
        }
    }
}
