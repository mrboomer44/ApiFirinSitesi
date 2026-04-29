using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        private readonly FırınContext _context;
        public CounterController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetCounters()
        {
            var counters = _context.Counters.ToList();
            return Ok(counters);
        }

        [HttpGet("{CounterId}")]
        public IActionResult GetClıent(int CounterId)
        {
            var counter = _context.Clıents.Find(CounterId);
            return Ok(counter);
        }

        [HttpPost]
        public IActionResult CreateCounter(Counter counter)
        {
            _context.Counters.Add(counter);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateCounter(Counter counter)
        {
            _context.Counters.Update(counter);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete]
        public IActionResult DeleteCounter(int CounterId)
        {
            _context.Counters.Remove(_context.Counters.Find(CounterId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
