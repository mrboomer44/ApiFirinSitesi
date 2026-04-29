using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiUı.ViewComponent
{
    public class ContactComponentPartial : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
