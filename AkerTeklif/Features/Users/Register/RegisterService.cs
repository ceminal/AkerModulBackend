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
                    if (!string.IsNullOrEmpty(registerDTO.Role))
                    {
                        var roleResult = await userManager.AddToRoleAsync(appUser, registerDTO.Role);

                        if (!roleResult.Succeeded)
                        {
                            return new RegisterResponse { Message = "Kullanıcı oluşturuldu ama rol atanırken hata oluştu." };
                        }
                    }

                    return new RegisterResponse()
                    {
                        Message = "Kayıt işlemi başarılı!"
                    };
                }
                else
                {
                    var errorDetails = string.Join(" | ", result.Errors.Select(e => e.Code + ": " + e.Description));
                    return new RegisterResponse()
                    {
                        Message = $"Kayıt başarısız! Hatalar: {errorDetails}"
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
