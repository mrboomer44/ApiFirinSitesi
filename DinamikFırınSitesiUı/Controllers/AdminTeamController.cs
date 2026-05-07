using DinamikFırınSitesiUı.Dtos.Banners;
using DinamikFırınSitesiUı.Dtos.Teams;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinamikFırınSitesiUı.Controllers
{
    public class AdminTeamController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminTeamController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseTeam = await client.GetAsync("https://localhost:7061/api/Team");
            if (responseTeam.IsSuccessStatusCode)
            {
                var jsonData = await responseTeam.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultTeamDto>>(jsonData);
                return View(values ?? new List<ResultTeamDto>());
            }
            return View(new List<ResultTeamDto>());
        }
        [HttpGet]
        public IActionResult AddTeam()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTeam(ResultTeamDto teamDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(teamDto);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7061/api/Team", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(teamDto);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateTeam(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7061/api/Team/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateTeamDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTeam(int id, UpdateTeamDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7061/api/Team", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7061/api/Team/{id}");
            return RedirectToAction("Index");
        }
    }
}