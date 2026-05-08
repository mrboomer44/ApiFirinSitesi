using DinamikFırınSitesiUı.Dtos.AboutList;
using DinamikFırınSitesiUı.Dtos.Adouts;
using DinamikFırınSitesiUı.Dtos.Counters;
using DinamikFırınSitesiUı.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinamikFırınSitesiUı.Controllers
{
    public class AdminAboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminAboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var viewModel = new AboutViewModel();


            var aboutResponse = await client.GetAsync("https://localhost:7061/api/About");
            if (aboutResponse.IsSuccessStatusCode)
            {
                var aboutJson = await aboutResponse.Content.ReadAsStringAsync();
                viewModel.Abouts = JsonConvert.DeserializeObject<List<ResultAboutDto>>(aboutJson) ?? new List<ResultAboutDto>();
            }

            var listResponse = await client.GetAsync("https://localhost:7061/api/AboutList");
            if (listResponse.IsSuccessStatusCode)
            {
                var listJson = await listResponse.Content.ReadAsStringAsync();
                viewModel.AboutLists = JsonConvert.DeserializeObject<List<ResultAboutListDto>>(listJson) ?? new List<ResultAboutListDto>();
            }

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult AddAbout()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAbout(CreateAboutDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7061/api/About", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // ÇÖZÜM: Boş dönen tekil GetById API'si yerine listeyi çekip eşleşeni alıyoruz
            var responseMessage = await client.GetAsync("https://localhost:7061/api/About");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UpdateAboutDto>>(jsonData);

                // Gelen ID ile eşleşen veriyi bul, yoksa listedeki ilk veriyi getir
                var currentData = values?.FirstOrDefault(x => x.AboutId == id) ?? values?.FirstOrDefault();

                if (currentData != null)
                {
                    return View(currentData);
                }
            }

            // API'den veri gelmezse formun null hatası vermemesi için boş instance dönüyoruz
            return View(new UpdateCounterDto());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            // API Update isteği
            var response = await client.PutAsync("https://localhost:7061/api/About", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult AddAboutList()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAboutList(CreateAboutListDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7061/api/AboutList", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateAboutList(int id)
        {
            var client = _httpClientFactory.CreateClient();
            // ÇÖZÜM: Boş dönen tekil GetById API'si yerine listeyi çekip eşleşeni alıyoruz
            var responseMessage = await client.GetAsync("https://localhost:7061/api/AboutList");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UpdateAboutListDto>>(jsonData);
                // Gelen ID ile eşleşen veriyi bul, yoksa listedeki ilk veriyi getir
                var currentData = values?.FirstOrDefault(x => x.AboutListId == id) ?? values?.FirstOrDefault();
                if (currentData != null)
                {
                    return View(currentData);
                }
            }
            // API'den veri gelmezse formun null hatası vermemesi için boş instance dönüyoruz
            return View(new UpdateAboutListDto());
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAboutList(UpdateAboutListDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            // API Update isteği
            var response = await client.PutAsync("https://localhost:7061/api/AboutList", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAboutList(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7061/api/AboutList/{id}");
            return RedirectToAction("Index");
        }
    }
}