using DinamikFırınSitesiUı.Dtos.Banners;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinamikFırınSitesiUı.Controllers
{
    public class AdminBannerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminBannerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var responseBanner = await client.GetAsync("https://localhost:7061/api/Banner");

            if (responseBanner.IsSuccessStatusCode)
            {
                var jsonData = await responseBanner.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBannerDto>>(jsonData);
                return View(values ?? new List<ResultBannerDto>());
            }


            return View(new List<ResultBannerDto>());
        }
        [HttpGet]
        public IActionResult AddBanner()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBanner(CreateBannerDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7061/api/Banner", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateBanner(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7061/api/Banner/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateBannerDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBanner(int id, UpdateBannerDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7061/api/Banner", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7061/api/Banner?BannerId={id}");
            return RedirectToAction("Index");
        }
    }
}
