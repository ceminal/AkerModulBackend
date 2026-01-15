using AkerTeklif.Features.Users.Login;
using AkerTeklif.Features.Users.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AkerTeklif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(RegisterService registerService, LoginService loginService) : ControllerBase
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
    }
}
