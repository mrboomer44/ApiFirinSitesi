using DinamikFırınSitesiUı.Dtos.Communications;
using DinamikFırınSitesiUı.Dtos.SocialMedias;
using System.Collections.Generic;

namespace DinamikFırınSitesiUı.Models
{
    public class ContactSettingsViewModel
    {
        public List<ResultCommunicationDto> Communications { get; set; } = new List<ResultCommunicationDto>();
        public List<ResultSocialMediaDto> SocialMedias { get; set; } = new List<ResultSocialMediaDto>();
    }
}