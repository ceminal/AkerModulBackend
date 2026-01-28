using AkerTeklif.Features.Users.DTOs;
using Microsoft.AspNetCore.Identity;

namespace AkerTeklif.Features.Users
{
    public class UserService(UserManager<AppUser> userManager)
    {
        public async Task<UpdateUserResponse> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            var user = await userManager.FindByIdAsync(updateUserDTO.Id.ToString());
            if (user == null)
                return new UpdateUserResponse { Message = "Kullanıcı bulunamadı."};

            // 1. Temel Bilgileri Güncelle
            user.UserName = updateUserDTO.UserName;
            user.Email = updateUserDTO.UserEmail;

            // 2. Şifre Güncelleme (Eğer yeni şifre girilmişse)
            if (!string.IsNullOrWhiteSpace(updateUserDTO.UserPassword))
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await userManager.ResetPasswordAsync(user, token, updateUserDTO.UserPassword);
                if (!passwordResult.Succeeded) return new UpdateUserResponse { Message = "Şifre güncelleriken bir hata meydana geldi."};
            }

            // 3. Rol Güncelleme Mantığı
            if (!string.IsNullOrWhiteSpace(updateUserDTO.RoleName))
            {
                // Kullanıcının mevcut tüm rollerini al
                var currentRoles = await userManager.GetRolesAsync(user);

                // Eğer yeni seçilen rol zaten kullanıcıda varsa tekrar işlem yapmaya gerek yok
                if (!currentRoles.Contains(updateUserDTO.RoleName))
                {
                    // Eski rolleri temizle (Genelde tek rol kullanılıyorsa bu yöntem tercih edilir)
                    await userManager.RemoveFromRolesAsync(user, currentRoles);

                    // Yeni rolü ekle
                    await userManager.AddToRoleAsync(user, updateUserDTO.RoleName);
                }
            }

            await userManager.UpdateAsync(user);

            return new UpdateUserResponse
            {
                Message = "Kullanıcı başarıyla güncellendi."
            };
        }
    }
}