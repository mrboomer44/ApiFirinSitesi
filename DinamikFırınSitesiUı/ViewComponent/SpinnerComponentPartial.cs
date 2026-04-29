
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiUı.ViewComponent
{
    public class SpinnerComponentPartial : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
