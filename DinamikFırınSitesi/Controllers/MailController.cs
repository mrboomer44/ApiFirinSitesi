using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly FırınContext _context;
        public MailController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetMails()
        {
            var mails = _context.Mail.ToList();
            return Ok(mails);
        }

        [HttpGet("{MailId}")]
        public IActionResult GetMail(int MailId)
        {
            var Mail = _context.Mail.Find(MailId);
            return Ok(Mail);
        }

        [HttpPost]
        public IActionResult CreateMail(Mail mail)
        {
            _context.Mail.Add(mail);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateMail(Mail mail)
        {
            _context.Mail.Update(mail);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete]
        public IActionResult DeleteMail(int MailId)
        {
            _context.Mail.Remove(_context.Mail.Find(MailId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
