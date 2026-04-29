using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiUı.ViewComponent
{
    public class NewsletterEmailComponentPartial : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.CompletedTask;
            return View();
        }
    }
}