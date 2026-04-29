using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicationController : ControllerBase
    {
        private readonly FırınContext _context;
        public CommunicationController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetCommunications()
        {
            var communications = _context.Communications.ToList();
            return Ok(communications);
        }

        [HttpGet("{CommunicationId}")]
        public IActionResult GetCommunication(int CommunicationId)
        {
            var Communication = _context.Communications.Find(CommunicationId);
            return Ok(Communication);
        }

        [HttpPost]
        public IActionResult CreateCommunication(Communication communication)
        {
            _context.Communications.Add(communication);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateCommunication(Communication communication)
        {
            _context.Communications.Update(communication);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete]
        public IActionResult DeleteCommunication(int CommunicationId)
        {
            _context.Communications.Remove(_context.Communications.Find(CommunicationId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
