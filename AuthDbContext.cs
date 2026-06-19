using Microsoft.EntityFrameworkCore;
using NUREM.Menu;

namespace NUREM.Auth
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; } // Hata veren servislerin aradığı doğru tablo ismi

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Varsayılan admin hesabını tohumluyoruz
            modelBuilder.Entity<AdminUser>().HasData(new AdminUser
            {
                Id = 1,
                Username = "admin",
                Password = "123"
            });
        }
    }
}