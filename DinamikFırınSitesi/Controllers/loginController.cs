using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using DinamikFırınSitesiAPI.Dal.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {
        private readonly FırınContext _context;
        public loginController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetLogins()
        {
            var logins = _context.logins.ToList();
            return Ok(logins);
        }

        [HttpGet("{loginId}")]
        public IActionResult GetLogin(int loginId)
        {
            var login = _context.logins.Find(loginId);
            return Ok(login);
        }

        [HttpPost]
        public IActionResult CreateLogin(login login)
        {
            _context.logins.Add(login);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateLogin(login login)
        {
            _context.logins.Update(login);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete]
        public IActionResult DeleteLogin(int loginId)
        {
            _context.logins.Remove(_context.logins.Find(loginId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
