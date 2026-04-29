using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using DinamikFırınSitesiAPI.Dal.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly FırınContext _context;
        public BannerController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetBanners()
        {
            var banners = _context.Banners.ToList();
            return Ok(banners);
        }

        [HttpGet("{BannerId}")]
        public IActionResult GetBanner(int BannerId)
        {
            var banner = _context.Banners.Find(BannerId);
            return Ok(banner);
        }

        [HttpPost]
        public IActionResult CreateBanner(Banner banner)
        {
            _context.Banners.Add(banner);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateBanner(Banner banner)
        {
            _context.Banners.Update(banner);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete]
        public IActionResult DeleteBanner(int BannerId)
        {
            _context.Banners.Remove(_context.Banners.Find(BannerId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
