using DinamikFırınSitesiUı.Dtos.Communications;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinamikFırınSitesiUı.ViewComponent
{
    public class NavbarComponentPartial : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public NavbarComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var communicationResponse = await client.GetAsync("https://localhost:7061/api/Communication");
            if (communicationResponse.IsSuccessStatusCode)
            {
                var jsonData = await communicationResponse.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCommunicationDto>>(jsonData);
                return View(values.FirstOrDefault());
            }
            return View();
        }
    }
}
