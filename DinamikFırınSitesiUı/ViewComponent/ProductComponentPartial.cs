using DinamikFırınSitesiUı.Dtos.Communications;
using DinamikFırınSitesiUı.Dtos.Products;
using DinamikFırınSitesiUı.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinamikFırınSitesiUı.ViewComponent
{
    public class ProductComponentPartial : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("FirinApi"); 
            var model = new ProductViewModel();

            var responseMessage = await client.GetAsync("api/Product");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                model.Products = values ?? new List<ResultProductDto>();
            }

            var communicationResponse = await client.GetAsync("api/Communication");
            if (communicationResponse.IsSuccessStatusCode)
            {
                var jsonData = await communicationResponse.Content.ReadAsStringAsync();
                var communicationValues = JsonConvert.DeserializeObject<List<ResultCommunicationDto>>(jsonData);
                model.Phone = communicationValues?.FirstOrDefault()?.Phone;
            }
            return View(model);
        }
    }
}
