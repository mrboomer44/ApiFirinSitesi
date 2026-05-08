using DinamikFırınSitesiUı.Dtos.Messages;

namespace DinamikFırınSitesiUı.Models
{
    public class DashboardMessageViewModel
    {
        public List<ResultMessageDto> UnreadMessages { get; set; } = new List<ResultMessageDto>();
        public List<ResultMessageDto> ReadMessages { get; set; } = new List<ResultMessageDto>();
    }
}