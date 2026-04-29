using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiUı.ViewComponent
{
    public class HeadComponentPartial : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
