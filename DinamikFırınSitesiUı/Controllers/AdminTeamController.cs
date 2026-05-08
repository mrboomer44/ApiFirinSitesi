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
            var client = _httpClientFactory.CreateClient("FirinApi");
            var responseTeam = await client.GetAsync("api/Team");
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
            var client = _httpClientFactory.CreateClient("FirinApi");
            var jsonData = JsonConvert.SerializeObject(teamDto);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Team", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(teamDto);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateTeam(int id)
        {
            var client = _httpClientFactory.CreateClient("FirinApi");
            var responseMessage = await client.GetAsync($"api/Team/{id}");
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
            var client = _httpClientFactory.CreateClient("FirinApi");
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"api/Team", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var client = _httpClientFactory.CreateClient("FirinApi");
            var response = await client.DeleteAsync($"api/Team/{id}");
            return RedirectToAction("Index");
        }
    }
}