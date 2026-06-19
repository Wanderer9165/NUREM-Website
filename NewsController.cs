using Microsoft.AspNetCore.Mvc;
using NUREM.News; // Yeni oluşturduğumuz haber modülünü çağırıyoruz

namespace NUREM_Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Yeni oluşturduğumuz Class1 içerisindeki metodu çağırıyoruz
            var newsService = new Class1();
            var mesaj = newsService.GetNews();
            return Ok(new { data = mesaj });
        }
    }
}