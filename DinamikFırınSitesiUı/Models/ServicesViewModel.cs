using DinamikFırınSitesiUı.Dtos.Services;
using DinamikFırınSitesiUı.Dtos.ServicesList;
using System.Collections.Generic;

namespace DinamikFırınSitesiUı.Models
{
    public class ServicesViewModel
    {
        public List<ResultServicesDto> Services { get; set; } = new List<ResultServicesDto>();
        public List<ResultServicesListDto> ServicesLists { get; set; } = new List<ResultServicesListDto>();
    }
}