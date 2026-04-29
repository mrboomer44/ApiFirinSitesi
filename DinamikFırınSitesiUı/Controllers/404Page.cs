using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiUı.Controllers
{
    public class _404Page : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
