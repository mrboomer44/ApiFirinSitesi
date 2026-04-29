using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly FırınContext _context;
        public SocialMediaController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetSocialMedias()
        {
            var socialMedias = _context.SocialMedias.ToList();
            return Ok(socialMedias);
        }

        [HttpGet("{SocialMediaId}")]
        public IActionResult GetSocialMedia(int SocialMediaId)
        {
            var socialMedia = _context.SocialMedias.Find(SocialMediaId);
            return Ok(socialMedia);
        }

        [HttpPost]
        public IActionResult CreateSocialMedia(SocialMedia socialMedia)
        {
            _context.SocialMedias.Add(socialMedia);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateSocialMedia(SocialMedia socialMedia)
        {
            _context.SocialMedias.Update(socialMedia);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete]
        public IActionResult DeleteSocialMedia(int SocialMediaId)
        {
            _context.SocialMedias.Remove(_context.SocialMedias.Find(SocialMediaId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
