using DinamikFırınSitesiUı.Dtos.AboutList;
using DinamikFırınSitesiUı.Dtos.Adouts;

namespace DinamikFırınSitesiUı.Models
{
    public class AboutViewModel
    {
        public List<ResultAboutDto> Abouts { get; set; } = new List<ResultAboutDto>();
        public List<ResultAboutListDto> AboutLists { get; set; } = new List<ResultAboutListDto>();
    }
}