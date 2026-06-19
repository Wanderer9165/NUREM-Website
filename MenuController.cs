using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUREM.Auth;
using NUREM.Menu;
using QRCoder;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NUREM_Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public MenuController(AuthDbContext context)
        {
            _context = context;
        }

        // 1. KAPI: Veritabanındaki Tüm Ürünleri Listeler
        [HttpGet("getir")]
        public async Task<IActionResult> GetMenu()
        {
            var urunListesi = await _context.Products.ToListAsync();
            return Ok(urunListesi);
        }

        // 2. KAPI: Yeni Ürün Ekleme (Admin Paneli İçin)
        [HttpPost("ekle")]
        public async Task<IActionResult> AddProduct([FromBody] Product yeniUrun)
        {
            if (yeniUrun == null) return BadRequest("Geçersiz ürün verisi.");

            _context.Products.Add(yeniUrun);
            await _context.SaveChangesAsync();
            return Ok(yeniUrun);
        }

        
        // 3. KAPI: Ürün Güncelleme (Geliştirilmiş Güvenli Sürüm)
        [HttpPut("guncelle/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product guncelUrun)
        {
            if (guncelUrun == null) return BadRequest("Geçersiz ürün verisi.");

            // Eğer JavaScript taraflı bir harf uyuşmazlığından dolayı id 0 geldiyse, URL'deki id'yi baz alıyoruz
            var hedefId = id;
            if (hedefId == 0 && guncelUrun.Id != 0) hedefId = guncelUrun.Id;

            var urun = await _context.Products.FindAsync(hedefId);
            if (urun == null) return NotFound($"Ürün bulunamadı. Aranan ID: {hedefId}");

            // Verileri güncelliyoruz
            urun.Name = guncelUrun.Name;
            urun.Description = guncelUrun.Description;
            urun.Price = guncelUrun.Price;
            urun.ImageUrl = guncelUrun.ImageUrl;
            urun.CategoryId = guncelUrun.CategoryId;

            await _context.SaveChangesAsync();
            return Ok(urun);
        }
        // 4. KAPI: Ürün Silme (Admin Paneli İçin)
        [HttpDelete("sil/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var urun = await _context.Products.FindAsync(id);
            if (urun == null) return NotFound("Ürün bulunamadı.");

            _context.Products.Remove(urun);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Ürün başarıyla silindi." });
        }

        // 5. KAPI: QR Kod Oluşturucu
        [HttpGet("qr-olustur")]
        public IActionResult CreateQrCode([FromQuery] string url = "https://7286/index.html")
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q))
                {
                    using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
                    {
                        byte[] qrCodeBytes = qrCode.GetGraphic(20);
                        return File(qrCodeBytes, "image/png");
                    }
                }
            }
        }
    }
}
