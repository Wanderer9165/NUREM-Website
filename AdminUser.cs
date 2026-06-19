using System.ComponentModel.DataAnnotations;

namespace NUREM.Auth
{
    public class AdminUser
    {
        [Key]
        public int Id { get;  set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // Güvenlik için düz metin şifre tutuyoruz (Geliştirme aşaması)
    }
}