using AkerTeklif.Features.Products;
using AkerTeklif.Features.Users;
using AkerTeklif.Features.Users.DTOs;
using AkerTeklif.Features.Users.Login;
using AkerTeklif.Features.Users.Register;
using AkerTeklif.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AkerTeklif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(RegisterService registerService, LoginService loginService, UserService userService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var register = await registerService.Register(registerDTO);
            return Ok(register);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var login = await loginService.Login(loginDTO);
            return Ok(login);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Roles.Admin}")]
        [HttpPut("userUpdate")]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO updateDTO)
        {
            var update = await userService.UpdateUser(updateDTO);
            return Ok(update);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Roles.Admin}")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userService.GetAllUsers();
            return Ok(users);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Roles.Admin}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await userService.DeleteUserHandler(id);
            return Ok(response);
        }

    }
}
