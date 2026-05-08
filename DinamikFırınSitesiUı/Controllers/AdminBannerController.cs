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
            var client = _httpClientFactory.CreateClient("FirinApi");

            var responseBanner = await client.GetAsync("/api/Banner");

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
            var client = _httpClientFactory.CreateClient("FirinApi");
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("/api/Banner", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateBanner(int id)
        {
            var client = _httpClientFactory.CreateClient("FirinApi");
            var responseMessage = await client.GetAsync($"/api/Banner/{id}");
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
            var client = _httpClientFactory.CreateClient("FirinApi");
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/Banner", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            var client = _httpClientFactory.CreateClient("FirinApi");
            var response = await client.DeleteAsync($"/api/Banner?BannerId={id}");
            return RedirectToAction("Index");
        }
    }
}
