using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly FırınContext _context;
        public ClientController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetClients()
        {
            var clients = _context.Clients.ToList();
            return Ok(clients);
        }

        [HttpGet("{clientId}")]
        public IActionResult GetClient(int clientId)
        {
            var client = _context.Clients.Find(clientId);
            return Ok(client);
        }

        [HttpPost]
        public IActionResult CreateClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateClient(Client client)
        {
            _context.Clients.Update(client);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete]
        public IActionResult DeleteClient(int clientId)
        {
            var entity = _context.Clients.Find(clientId);
            if (entity == null)
            {
                return NotFound("Kayıt bulunamadı.");
            }
            _context.Clients.Remove(entity);
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
