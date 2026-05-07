using DinamikFırınSitesiUı.Dtos.Clients;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinamikFırınSitesiUı.Controllers
{
    public class AdminClientController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminClientController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseClient = await client.GetAsync("https://localhost:7061/api/Client");
            if (responseClient.IsSuccessStatusCode)
            {
                var jsonData = await responseClient.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultClientDto>>(jsonData);
                return View(values ?? new List<ResultClientDto>());
            }
            return View(new List<ResultClientDto>());
        }
        [HttpGet]
        public IActionResult AddClient()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddClient(CreateClientDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7061/api/Client", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateClient(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7061/api/Client/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateClientDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateClient(int id, UpdateClientDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7061/api/Client", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7061/api/Client?ClientId={id}");
            return RedirectToAction("Index");
        }
    }
}