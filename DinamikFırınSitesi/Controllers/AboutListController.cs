using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutListController : ControllerBase
    {
        private readonly FırınContext _context;
        public AboutListController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAboutList()
        {
            var aboutList = _context.AboutLists.ToList();
            return Ok(aboutList);
        }

        [HttpGet("{AboutListId}")]
        public IActionResult GetAboutList(int AboutListId)
        {
            var AboutList = _context.AboutLists.Find(AboutListId);
            return Ok(AboutList);
        }

        [HttpPost]
        public IActionResult CreateAboutList(AboutList aboutList)
        {
            _context.AboutLists.Add(aboutList);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateAboutList(AboutList aboutList)
        {
            _context.AboutLists.Update(aboutList);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete("{AboutListId}")]
        public IActionResult DeleteAboutList(int AboutListId)
        {
            _context.AboutLists.Remove(_context.AboutLists.Find(AboutListId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
