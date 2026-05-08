using System.Text;
using DinamikFırınSitesiUı.Dtos.NewsletterEmail;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinamikFırınSitesiUı.Controllers
{
    public class Default : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Default(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewsletterSubscribe(CreateNewsletterEmailDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Emails))
            {
                TempData["NewsletterMessage"] = "Lutfen gecerli bir e-posta giriniz.";
                return RedirectToAction("Index");
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("/api/NewsletterEmail", content);

            TempData["NewsletterMessage"] = responseMessage.IsSuccessStatusCode
                ? "E-posta aboneligi basariyla kaydedildi."
                : "Kayit sirasinda bir hata olustu.";

            return RedirectToAction("Index");
        }
    }
}
