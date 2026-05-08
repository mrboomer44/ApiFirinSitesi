using DinamikFırınSitesiUı.Dtos.Communications;
using DinamikFırınSitesiUı.Dtos.SocialMedias;

namespace DinamikFırınSitesiUı.Models
{
    public class ContactSettingsViewModel
    {
        public List<ResultCommunicationDto> Communications { get; set; } = new List<ResultCommunicationDto>();
        public List<ResultSocialMediaDto> SocialMedias { get; set; } = new List<ResultSocialMediaDto>();
    }
}