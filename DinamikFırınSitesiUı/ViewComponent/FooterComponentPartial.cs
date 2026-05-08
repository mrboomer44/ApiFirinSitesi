using DinamikFırınSitesiUı.Dtos.Communications;
using DinamikFırınSitesiUı.Dtos.Galerys;
using DinamikFırınSitesiUı.Dtos.SocialMedias;
using DinamikFırınSitesiUı.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DinamikFırınSitesiUı.ViewComponent
{
    public class FooterComponentPartial : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FooterComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("FirinApi");
            var model = new FooterViewModel();

            var communicationResponse = await client.GetAsync("api/Communication");
            if (communicationResponse.IsSuccessStatusCode)
            {
                var jsonData = await communicationResponse.Content.ReadAsStringAsync();
                var communicationValues = JsonConvert.DeserializeObject<List<ResultCommunicationDto>>(jsonData);
                model.Communication = communicationValues?.FirstOrDefault();
            }

            var socialMediaResponse = await client.GetAsync("api/SocialMedia");
            if (socialMediaResponse.IsSuccessStatusCode)
            {
                var jsonData = await socialMediaResponse.Content.ReadAsStringAsync();
                var socialMediaValues = JsonConvert.DeserializeObject<List<ResultSocialMediaDto>>(jsonData);
                model.SocialMedia = socialMediaValues?.FirstOrDefault();
            }

            var galeryResponse = await client.GetAsync("api/Galery");
            if (galeryResponse.IsSuccessStatusCode)
            {
                var jsonData = await galeryResponse.Content.ReadAsStringAsync();
                var galeryValues = JsonConvert.DeserializeObject<List<ResultGaleryDto>>(jsonData);
                model.Galeries = galeryValues ?? new List<ResultGaleryDto>();
            }

            return View(model);
        }
    }
}
