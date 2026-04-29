using System.Text;
using DinamikFırınSitesiUı.Dtos.Messages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinamikFırınSitesiUı.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.Mail) ||
                string.IsNullOrWhiteSpace(dto.Subject) ||
                string.IsNullOrWhiteSpace(dto.MessageContent))
            {
                TempData["ContactMessage"] = "Lutfen tum alanlari doldurunuz.";
                return RedirectToAction("Index");
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7061/api/Message", content);

            TempData["ContactMessage"] = responseMessage.IsSuccessStatusCode
                ? "Mesajiniz basariyla gonderildi."
                : "Mesaj gonderilirken bir hata olustu.";

            return RedirectToAction("Index");
        }
    }
}