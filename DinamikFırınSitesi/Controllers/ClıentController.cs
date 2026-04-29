using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClıentController : ControllerBase
    {
        private readonly FırınContext _context;
        public ClıentController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetClıents()
        {
            var clıents = _context.Clıents.ToList();
            return Ok(clıents);
        }

        [HttpGet("{ClıentId}")]
        public IActionResult GetClıent(int ClıentId)
        {
            var clıent = _context.Clıents.Find(ClıentId);
            return Ok(clıent);
        }

        [HttpPost]
        public IActionResult CreateClıent(Clıent clıent)
        {
            _context.Clıents.Add(clıent);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateCLıent(Clıent clıent)
        {
            _context.Clıents.Update(clıent);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete]
        public IActionResult DeleteClıent(int ClıentId)
        {
            _context.Clıents.Remove(_context.Clıents.Find(ClıentId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
