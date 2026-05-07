using DinamikFırınSitesiUı.Dtos.Galerys;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinamikFırınSitesiUı.Controllers
{
    public class AdminGaleryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminGaleryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseGalery = await client.GetAsync("https://localhost:7061/api/Galery");
            if (responseGalery.IsSuccessStatusCode)
            {
                var jsonData = await responseGalery.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultGaleryDto>>(jsonData);
                return View(values ?? new List<ResultGaleryDto>());
            }
            return View(new List<ResultGaleryDto>());
        }
        [HttpGet]
        public IActionResult AddGalery()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGalery(CreateGaleryDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7061/api/Galery", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateGalery(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7061/api/Galery/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateGaleryDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateGalery(UpdateGaleryDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://localhost:7061/api/Galery/{model.GaleryId}", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteGalery(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7061/api/Galery/{id}");
                return RedirectToAction("Index");
        }
    }
}