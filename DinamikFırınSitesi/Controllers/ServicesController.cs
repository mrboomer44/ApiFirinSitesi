using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly FırınContext _context;
        public ServicesController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetServicess()
        {
            var services = _context.Services.ToList();
            return Ok(services);
        }

        [HttpGet("{ServicesId}")]
        public IActionResult GetServices(int ServicesId)
        {
            var services = _context.Services.Find(ServicesId);
            return Ok(services);
        }

        [HttpPost]
        public IActionResult CreateServices(Services services)
        {
            _context.Services.Add(services);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateServices(Services services)
        {
            _context.Services.Update(services);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete]
        public IActionResult DeleteServices(int ServicesId)
        {
            _context.Services.Remove(_context.Services.Find(ServicesId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
