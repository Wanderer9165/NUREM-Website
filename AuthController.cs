using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Kırmızı çizgiyi kaldıran kritik kütüphane
using NUREM.Auth;
using System.Threading.Tasks;

namespace NUREM_Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public AuthController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null) return BadRequest("Geçersiz istek.");

            // Doğrudan veritabanındaki AdminUsers tablosuna güvenli asenkron sorgu atıyoruz
            var user = await _context.AdminUsers
                .FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized(new { success = false, message = "Kullanıcı adı veya şifre hatalı!" });
            }

            return Ok(new { success = true, message = "Giriş başarılı!" });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}