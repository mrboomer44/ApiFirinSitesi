using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesListController : ControllerBase
    {
        private readonly FırınContext _context;
        public ServicesListController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetServicesLists()
        {
            var servicesList = _context.ServicesList.ToList();
            return Ok(servicesList);
        }

        [HttpGet("{ServicesListId}")]
        public IActionResult GetServicesList(int ServicesListId)
        {
            var servicesList = _context.ServicesList.Find(ServicesListId);
            return Ok(servicesList);
        }

        [HttpPost]
        public IActionResult CreateServicesList(ServicesList servicesList)
        {
            _context.ServicesList.Add(servicesList);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateServicesList(ServicesList servicesList)
        {
            _context.ServicesList.Update(servicesList);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete]
        public IActionResult DeleteServicesList(int ServicesListId)
        {
            _context.ServicesList.Remove(_context.ServicesList.Find(ServicesListId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
