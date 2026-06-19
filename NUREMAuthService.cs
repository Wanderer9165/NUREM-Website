using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace NUREM.Auth
{
    public class NUREMAuthService
    {
        private readonly AuthDbContext _context;

        public NUREMAuthService(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            // CRITICAL FIX: _context.Users yerine veritabanındaki gerçek tablo adı olan AdminUsers'ı kullanıyoruz
            var user = await _context.AdminUsers
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            return user != null;
        }
    }
}