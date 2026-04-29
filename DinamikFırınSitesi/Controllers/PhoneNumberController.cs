using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using DinamikFırınSitesiAPI.Dal.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumberController : ControllerBase
    {
        private readonly FırınContext _context;
        public PhoneNumberController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetPhoneNumbers()
        {
            var phoneNumbers = _context.PhoneNumbers.ToList();
            return Ok(phoneNumbers);
        }

        [HttpGet("{PhoneNumberId}")]
        public IActionResult GetPhoneNumber(int PhoneNumberId)
        {
            var phoneNumber = _context.PhoneNumbers.Find(PhoneNumberId);
            return Ok(phoneNumber);
        }

        [HttpPost]
        public IActionResult CreatePhoneNumber(PhoneNumber phoneNumber)
        {
            _context.PhoneNumbers.Add(phoneNumber);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            _context.PhoneNumbers.Update(phoneNumber);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete]
        public IActionResult DeletePhoneNumber(int PhoneNumberId)
        {
            _context.PhoneNumbers.Remove(_context.PhoneNumbers.Find(PhoneNumberId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
