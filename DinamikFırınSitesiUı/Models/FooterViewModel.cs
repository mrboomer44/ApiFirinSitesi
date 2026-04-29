using DinamikFırınSitesiUı.Dtos.Communications;
using DinamikFırınSitesiUı.Dtos.Galerys;
using DinamikFırınSitesiUı.Dtos.SocialMedias;

namespace DinamikFırınSitesiUı.Models
{
    public class FooterViewModel
    {
        public ResultCommunicationDto? Communication { get; set; }
        public ResultSocialMediaDto? SocialMedia { get; set; }
        public List<ResultGaleryDto> Galeries { get; set; } = new();
    }
}
