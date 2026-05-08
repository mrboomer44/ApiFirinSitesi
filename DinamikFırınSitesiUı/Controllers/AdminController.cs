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
            var client = _httpClientFactory.CreateClient("FirinApi");

            var responseMessage = await client.GetAsync("/api/Message");

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
            var client = _httpClientFactory.CreateClient("FirinApi");
            var responseMessage = await client.PutAsync($"/api/Message/MarkAsRead/{id}", null);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            var client = _httpClientFactory.CreateClient("FirinApi");
            var responseMessage = await client.DeleteAsync($"/api/Message?MessageId={id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Default");
        }
    }
}