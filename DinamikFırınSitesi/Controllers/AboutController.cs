using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly FırınContext _context;
        public AboutController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAbouts()
        {
            var abouts = _context.Abouts.ToList();
            return Ok(abouts);
        }

        [HttpGet("{AboutId}")]
        public IActionResult GetAbout(int AboutId)
        {
            var About = _context.Abouts.Find(AboutId);
            return Ok(About);
        }

        [HttpPost]
        public IActionResult CreateAbout(About about)
        {
            _context.Abouts.Add(about);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateAbout(About about)
        {
            _context.Abouts.Update(about);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete]
        public IActionResult DeleteAbout(int AboutId)
        {
            _context.Abouts.Remove(_context.Abouts.Find(AboutId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
