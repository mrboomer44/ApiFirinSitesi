namespace DinamikFırınSitesiUı.Dtos.Products
{
    public class UpdateProductDto
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StrartPrice { get; set; }
        public string EndPrice { get; set; }
        public string ImageUrl { get; set; }
    }
}
