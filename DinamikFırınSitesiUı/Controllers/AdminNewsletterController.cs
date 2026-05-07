using DinamikFırınSitesiUı.Dtos.NewsletterEmail;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinamikFırınSitesiUı.Controllers
{
    public class AdminNewsletterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminNewsletterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseNewsletter = await client.GetAsync("https://localhost:7061/api/NewsletterEmail");
            if (responseNewsletter.IsSuccessStatusCode)
            {
                var jsonData = await responseNewsletter.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultNewsletterEmailDto>>(jsonData);
                return View(values ?? new List<ResultNewsletterEmailDto>());
            }
            return View(new List<ResultNewsletterEmailDto>());
        }
        [HttpGet]
        public async Task<IActionResult> DeleteNewsletter(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7061/api/NewsletterEmail?NewsletterEmailId={id}");
            return RedirectToAction("Index");
        }
    }
}
