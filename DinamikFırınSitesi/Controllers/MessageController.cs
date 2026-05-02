using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly FırınContext _context;
        public MessageController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetMessages()
        {
            var messages = _context.Messages.ToList();
            return Ok(messages);
        }

        [HttpGet("{MessageId}")]
        public IActionResult GetMessage(int MessageId)
        {
            var Message = _context.Messages.Find(MessageId);
            return Ok(Message);
        }

        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            message.Read = false;  // Yeni mesaj her zaman okunmamış başlar
            _context.Messages.Add(message);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateMessage(Message message)
        {
            _context.Messages.Update(message);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete]
        public IActionResult DeleteMessage(int MessageId)
        {
            _context.Messages.Remove(_context.Messages.Find(MessageId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
