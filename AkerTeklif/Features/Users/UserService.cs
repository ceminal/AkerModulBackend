using AkerTeklif.Data;
using AkerTeklif.Features.Users.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AkerTeklif.Features.Users
{
    public class UserService(UserManager<AppUser> userManager, AppDbContext context)
    {
        public async Task<UpdateUserResponse> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            var user = await userManager.FindByIdAsync(updateUserDTO.Id.ToString());
            if (user == null)
                return new UpdateUserResponse { Message = "Kullanıcı bulunamadı." };

            user.UserName = updateUserDTO.UserName;
            user.Email = updateUserDTO.UserEmail;

            //if (!string.IsNullOrWhiteSpace(updateUserDTO.UserPassword) &&
            //    updateUserDTO.UserPassword == updateUserDTO.ConfirmPassword)
            //{
            //    var token = await userManager.GeneratePasswordResetTokenAsync(user);
            //    var passwordResult = await userManager.ResetPasswordAsync(user, token, updateUserDTO.UserPassword);
            //    if (!passwordResult.Succeeded)
            //        return new UpdateUserResponse { Message = "Şifre güncellenemedi." };
            //}

            if (!string.IsNullOrWhiteSpace(updateUserDTO.Role))
            {
                var currentRoles = await userManager.GetRolesAsync(user);

                if (!currentRoles.Contains(updateUserDTO.Role))
                {
                    await userManager.RemoveFromRolesAsync(user, currentRoles);
                    await userManager.AddToRoleAsync(user, updateUserDTO.Role);
                }
            }

            var result = await userManager.UpdateAsync(user);
            return new UpdateUserResponse
            {
                Message = result.Succeeded ? "Kullanıcı başarıyla güncellendi." : "Güncelleme sırasında hata oluştu."
            };
        }

        public async Task<List<GetUserDTO>> GetAllUsers()
        {
            var users = await userManager.Users.ToListAsync();
            var result = new List<GetUserDTO>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);

                result.Add(new GetUserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName ?? "",
                    UserEmail = user.Email ?? "",
                    Role = roles.FirstOrDefault()!
                });
            }

            return result;
        }

        public async Task<UpdateUserResponse> DeleteUserHandler(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return new UpdateUserResponse { Message = "Silinmek istenen kullanıcı bulunamadı." };
            }

            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new UpdateUserResponse { Message = $"Silme hatası: {errors}" };
            }

            return new UpdateUserResponse { Message = "Kullanıcı ve ilişkili Identity verileri başarıyla silindi." };
        }
    }
}