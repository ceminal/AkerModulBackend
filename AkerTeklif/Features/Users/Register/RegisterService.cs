using Microsoft.AspNetCore.Identity;

namespace AkerTeklif.Features.Users.Register
{
    public class RegisterService(UserManager<AppUser> userManager)
    {
        public async Task<RegisterResponse> Register(RegisterDTO registerDTO)
        {
            if (registerDTO.Password == registerDTO.ConfirmPassword)
            {
                var appUser = new AppUser
                {
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email,
                };

                var result = await userManager.CreateAsync(appUser, registerDTO.Password);

                if (result.Succeeded)
                {
                    return new RegisterResponse()
                    {
                        Message = "Kayıt işlemi başarılı!"
                    };
                }
                else
                {
                    return new RegisterResponse()
                    {
                        Message = "Kayıt işlemi başarısız!"
                    };
                }
            }
            else
            {
                return new RegisterResponse()
                {
                    Message = "Şifreler eşleşmiyor!"
                };
            }
        }
    }
}
