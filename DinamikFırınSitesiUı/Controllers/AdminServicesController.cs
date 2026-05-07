using DinamikFırınSitesiUı.Dtos.Adouts;
using DinamikFırınSitesiUı.Dtos.Counters;
using DinamikFırınSitesiUı.Dtos.Services;
using DinamikFırınSitesiUı.Dtos.ServicesList;
using DinamikFırınSitesiUı.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinamikFırınSitesiUı.Controllers
{
    public class AdminServicesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminServicesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var viewModel = new ServicesViewModel();

            var response = await client.GetAsync("https://localhost:7061/api/Services");
            if (response.IsSuccessStatusCode)
            {
                var ServicesJson = await response.Content.ReadAsStringAsync();
                viewModel.Services = JsonConvert.DeserializeObject<List<ResultServicesDto>>(ServicesJson);
            }

            var responseList = await client.GetAsync("https://localhost:7061/api/ServicesList");
            if (responseList.IsSuccessStatusCode)
            {
                var ServicesListJson = await responseList.Content.ReadAsStringAsync();
                viewModel.ServicesLists = JsonConvert.DeserializeObject<List<ResultServicesListDto>>(ServicesListJson);
            }

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult AddServices()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddServices(CreateServicesDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7061/api/Services", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateServices(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // ÇÖZÜM: Boş dönen tekil GetById API'si yerine listeyi çekip eşleşeni alıyoruz
            var responseMessage = await client.GetAsync("https://localhost:7061/api/Services");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UpdateServicesDto>>(jsonData);

                // Gelen ID ile eşleşen veriyi bul, yoksa listedeki ilk veriyi getir
                var currentData = values?.FirstOrDefault(x => x.ServicesId == id) ?? values?.FirstOrDefault();

                if (currentData != null)
                {
                    return View(currentData);
                }
            }

            // API'den veri gelmezse formun null hatası vermemesi için boş instance dönüyoruz
            return View(new UpdateServicesDto());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateServices(UpdateServicesDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            // API Update isteği
            var response = await client.PutAsync("https://localhost:7061/api/Services", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult AddServicesList()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddServicesList(CreateServicesListDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7061/api/ServicesList", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateServicesList(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // ÇÖZÜM: Boş dönen tekil GetById API'si yerine listeyi çekip eşleşeni alıyoruz
            var responseMessage = await client.GetAsync("https://localhost:7061/api/ServicesList");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UpdateServicesListDto>>(jsonData);

                // Gelen ID ile eşleşen veriyi bul, yoksa listedeki ilk veriyi getir
                var currentData = values?.FirstOrDefault(x => x.ServicesListId == id) ?? values?.FirstOrDefault();
                if (currentData != null)
                {
                    return View(currentData);
                }
            }

            // API'den veri gelmezse formun null hatası vermemesi için boş instance dönüyoruz
            return View(new UpdateServicesListDto());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateServicesList(UpdateServicesListDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            // API Update isteği
            var response = await client.PutAsync("https://localhost:7061/api/ServicesList", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteServicesList(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7061/api/ServicesList/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
