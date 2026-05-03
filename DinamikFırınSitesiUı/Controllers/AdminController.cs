using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using DinamikFırınSitesiUı.Dtos.Messages;
using DinamikFırınSitesiUı.Models;

namespace DinamikFırınSitesiUı.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("https://localhost:7061/api/Message");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);

                var viewModel = new DashboardMessageViewModel
                {
                    UnreadMessages = values.Where(m => !m.Read).ToList(),
                    ReadMessages = values.Where(m => m.Read).ToList()
                };

                return View(viewModel); 
            }


            return View(new DashboardMessageViewModel());
        }
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var getResponse = await client.GetAsync($"https://localhost:7061/api/Message/{id}");
            if (!getResponse.IsSuccessStatusCode)
            {
                TempData["AdminMessageError"] = "Mesaj bulunamadı.";
                return RedirectToAction("Index");
            }

            var jsonData = await getResponse.Content.ReadAsStringAsync();
            var message = JsonConvert.DeserializeObject<UpdateMessageDto>(jsonData);

            if (message == null)
            {
                TempData["AdminMessageError"] = "Mesaj bilgisi okunamadı.";
                return RedirectToAction("Index");
            }

            message.Read = true;
            var content = new StringContent(JsonConvert.SerializeObject(message), System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7061/api/Message", content);

            if (!responseMessage.IsSuccessStatusCode)
            {
                TempData["AdminMessageError"] = "Mesaj okundu olarak işaretlenemedi.";
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteMessage(int id) 
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7061/api/Message?MessageId={id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Default");
        }
    }
}
