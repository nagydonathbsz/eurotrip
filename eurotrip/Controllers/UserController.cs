using eurotrip.Auth;
using eurotrip.Modell;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eurotrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        EuroContext _context;

        public UserController(EuroContext context)
        {
            _context = context;
        }
        [Authorize(Policy = "User.Read")]
        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var list = await _context.Users.ToListAsync();
            return Ok(list);
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetUserListMe()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            if (email == null) return Unauthorized();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return NotFound();

            return Ok(user);
        }
        [Authorize(Policy = "User.ReadId")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserListById(int id)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> PostRegister([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) ||
                string.IsNullOrWhiteSpace(user.Password) ||
                string.IsNullOrWhiteSpace(user.Email)) return BadRequest("Hiányzó adatok!");

            // 1. Ellenőrizzük, létezik-e már a felhasználónév VAGY az email
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Name == user.Name || u.Email == user.Email);

            if (existingUser != null)
            {
                // Pontosabb hibaüzenet (opcionális)
                if (existingUser.Email == user.Email)
                    return BadRequest("Ez az email cím már regisztrálva van!");

                return BadRequest("Ez a felhasználónév már foglalt!");
            }

            // 2. Jelszó hashelése és mentés
            user.Password = PasswordHandler.HashPassword(user.Password);
            user.isAdmin = 0; // Biztonság kedvéért kényszerítsük sima felhasználónak

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Created($"{Request.GetDisplayUrl()}/{user.Id}", user);
        }
        [Authorize]
        [HttpDelete("me")]
        public async Task<IActionResult> DeleteMe()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            if (email == null) return Unauthorized();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Policy = "User.Update")]
        [HttpPut("me")]
        public async Task<IActionResult> PutUser(User user)
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            if (email == null) return Unauthorized();
            var oldUser = await _context.Users.FirstOrDefaultAsync(p => p.Email == email);
            if (oldUser == null) return NotFound();
            oldUser.Name = user.Name;
            oldUser.Email = user.Email;
            if (!string.IsNullOrWhiteSpace(user.Password))
                oldUser.Password = PasswordHandler.HashPassword(user.Password);
            oldUser.Phone = user.Phone;
            await _context.SaveChangesAsync();
            return Ok(oldUser);
        }
    }
}
