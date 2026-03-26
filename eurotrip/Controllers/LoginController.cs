using eurotrip.Auth;
using eurotrip.Modell;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eurotrip.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private EuroContext _context;
        private readonly TokenManager _tokenManager;

        public LoginController(EuroContext context, TokenManager tokenManager)
        {
            _context = context;
            _tokenManager = tokenManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestData ue)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == ue.UserId);
            if (user == null || !PasswordHandler.VerifyPassword(ue.Password, user.Password)) return Unauthorized();
            var token = _tokenManager.GenerateToken(user);
            return Ok(token);
        }

        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}
