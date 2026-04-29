using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using DinamikFırınSitesiAPI.Dal.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsletterEmailController : ControllerBase
    {
        private readonly FırınContext _context;
        public NewsletterEmailController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetNewsletterEmails()
        {
            var newsletterEmails = _context.NewsletterEmails.ToList();
            return Ok(newsletterEmails);
        }

        [HttpGet("{NewsletterEmailId}")]
        public IActionResult GetNewsletterEmail(int NewsletterEmailId)
        {
            var newsletterEmail = _context.NewsletterEmails.Find(NewsletterEmailId);
            return Ok(newsletterEmail);
        }

        [HttpPost]
        public IActionResult CreateNewsletterEmail(NewsletterEmail newsletterEmail)
        {
            _context.NewsletterEmails.Add(newsletterEmail);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateNewsletterEmail(NewsletterEmail newsletterEmail)
        {
            _context.NewsletterEmails.Update(newsletterEmail);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete]
        public IActionResult DeleteNewsletterEmail(int NewsletterEmailId)
        {
            _context.NewsletterEmails.Remove(_context.NewsletterEmails.Find(NewsletterEmailId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
