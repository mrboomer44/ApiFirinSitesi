using DinamikFırınSitesiUı.Dtos.Adouts;
using DinamikFırınSitesiUı.Dtos.Communications;
using DinamikFırınSitesiUı.Dtos.Counters;
using DinamikFırınSitesiUı.Dtos.SocialMedias;
using DinamikFırınSitesiUı.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinamikFırınSitesiUı.Controllers
{
    public class AdminContactSettingsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminContactSettingsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("FirinApi");
            var viewModel = new ContactSettingsViewModel();

            var responseCommunication = await client.GetAsync("/api/Communication");
            if (responseCommunication.IsSuccessStatusCode)
            {
                var jsonData = await responseCommunication.Content.ReadAsStringAsync();
                viewModel.Communications = JsonConvert.DeserializeObject<List<ResultCommunicationDto>>(jsonData);
            }

            var responseSocialMedia = await client.GetAsync("/api/SocialMedia");
            if (responseSocialMedia.IsSuccessStatusCode)
            {
                var jsonData = await responseSocialMedia.Content.ReadAsStringAsync();
                viewModel.SocialMedias = JsonConvert.DeserializeObject<List<ResultSocialMediaDto>>(jsonData);
            }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult AddCommunication()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCommunication(CreateCommunicationDto model)
        {
            var client = _httpClientFactory.CreateClient("FirinApi");
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("/api/Communication", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddSocialMedia()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSocialMedia(CreateSocialMediaDto model)
        {
            var client = _httpClientFactory.CreateClient("FirinApi");
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("/api/SocialMedia", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCommunication(int id)
        {
            var client = _httpClientFactory.CreateClient("FirinApi");

            // ÇÖZÜM: Boş dönen tekil GetById API'si yerine listeyi çekip eşleşeni alıyoruz
            var responseMessage = await client.GetAsync("/api/Communication");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UpdateCommunicationDto>>(jsonData);

                // Gelen ID ile eşleşen veriyi bul, yoksa listedeki ilk veriyi getir
                var currentData = values?.FirstOrDefault(x => x.CommunicationId == id) ?? values?.FirstOrDefault();

                if (currentData != null)
                {
                    return View(currentData);
                }
            }

            // API'den veri gelmezse formun null hatası vermemesi için boş instance dönüyoruz
            return View(new UpdateCommunicationDto());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCommunication(UpdateCommunicationDto model)
        {
            var client = _httpClientFactory.CreateClient("FirinApi");
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            // API Update isteği
            var response = await client.PutAsync("/api/Communication", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateSocialMedia(int id)
        {
            var client = _httpClientFactory.CreateClient("FirinApi");

            // ÇÖZÜM: Boş dönen tekil GetById API'si yerine listeyi çekip eşleşeni alıyoruz
            var responseMessage = await client.GetAsync("/api/SocialMedia");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UpdateSocialMediaDto>>(jsonData);

                // Gelen ID ile eşleşen veriyi bul, yoksa listedeki ilk veriyi getir
                var currentData = values?.FirstOrDefault(x => x.SocialMediaId == id) ?? values?.FirstOrDefault();

                if (currentData != null)
                {
                    return View(currentData);
                }
            }

            // API'den veri gelmezse formun null hatası vermemesi için boş instance dönüyoruz
            return View(new UpdateSocialMediaDto());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaDto model)
        {
            var client = _httpClientFactory.CreateClient("FirinApi");
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            // API Update isteği
            var response = await client.PutAsync("/api/SocialMedia", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
