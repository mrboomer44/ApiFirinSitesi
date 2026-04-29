namespace DinamikFırınSitesiUı.Dtos.Banners
{
    public class UpdateBannerDto
    {
        public int BannerId { get; set; }
        public string Ticket { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
