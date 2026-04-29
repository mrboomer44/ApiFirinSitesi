using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiUı.ViewComponent
{
    public class CopyrightComponentPartial : Microsoft.AspNetCore.Mvc.ViewComponent    
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
