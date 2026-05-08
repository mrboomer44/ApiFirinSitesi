using DinamikFırınSitesiUı.Dtos.Banners;
using DinamikFırınSitesiUı.Dtos.Counters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DinamikFırınSitesiUı.Controllers
{
    public class AdminCounterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCounterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("FirinApi");
            var responseMessage = await client.GetAsync("/api/Counter");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCounterDto>>(jsonData);

                // ÇÖZÜM: Boş, sahte bir nesne yerine doğrudan listeyi View'a gönderiyoruz.
                return View(values ?? new List<ResultCounterDto>());
            }

            return View(new List<ResultCounterDto>());
        }

        [HttpGet]
        public IActionResult AddCounter()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCounter(CreateCounterDto model)
        {
            var client = _httpClientFactory.CreateClient("FirinApi");
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("/api/Counter", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCounter(int id)
        {
            var client = _httpClientFactory.CreateClient("FirinApi");

            // ÇÖZÜM: Boş dönen tekil GetById API'si yerine listeyi çekip eşleşeni alıyoruz
            var responseMessage = await client.GetAsync("/api/Counter");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UpdateCounterDto>>(jsonData);

                // Gelen ID ile eşleşen veriyi bul, yoksa listedeki ilk veriyi getir
                var currentData = values?.FirstOrDefault(x => x.CounterId == id) ?? values?.FirstOrDefault();

                if (currentData != null)
                {
                    return View(currentData);
                }
            }

            // API'den veri gelmezse formun null hatası vermemesi için boş instance dönüyoruz
            return View(new UpdateCounterDto());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCounter(UpdateCounterDto model)
        {
            var client = _httpClientFactory.CreateClient("FirinApi");
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            // API Update isteği
            var response = await client.PutAsync("/api/Counter", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}