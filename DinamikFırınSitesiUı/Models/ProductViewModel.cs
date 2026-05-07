using DinamikFırınSitesiUı.Dtos.Products;

namespace DinamikFırınSitesiUı.Models
{
    public class ProductViewModel
    {
        public List<ResultProductDto> Products { get; set; } = new List<ResultProductDto>();
        public string? Phone { get; set; }

    }
}
