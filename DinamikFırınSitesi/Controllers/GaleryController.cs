using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GaleryController : ControllerBase
    {
        private readonly FırınContext _context;
        public GaleryController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetGalerys()
        {
            var galerys = _context.Galeries.ToList();
            return Ok(galerys);
        }

        [HttpGet("{GaleryId}")]
        public IActionResult GetGalery(int GaleryId)
        {
            var galery = _context.Galeries.Find(GaleryId);
            return Ok(galery);
        }

        [HttpPost]
        public IActionResult CreateGalery(Galery galery)
        {
            _context.Galeries.Add(galery);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateGalery(Galery galery)
        {
            _context.Galeries.Update(galery);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete("{GaleryId}")]
        public IActionResult DeleteGalery(int GaleryId)
        {
            _context.Galeries.Remove(_context.Galeries.Find(GaleryId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
