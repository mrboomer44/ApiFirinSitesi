using AkademiqDinamikFırınSitesiApi.Dal.Entitys;
using DinamikFırınSitesi.Dal.Context;
using Microsoft.AspNetCore.Mvc;

namespace DinamikFırınSitesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly FırınContext _context;
        public ProductController(FırınContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{ProductId}")]
        public IActionResult GetProduct(int ProductId)
        {
            var product = _context.Products.Find(ProductId);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok("Ekleme işlemi tamamlandı");
        }

        [HttpPut]
        public IActionResult UpdateAbout(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi tamamlandı");
        }

        [HttpDelete("{ProductId}")]
        public IActionResult DeleteProduct(int ProductId)
        {
            _context.Products.Remove(_context.Products.Find(ProductId));
            _context.SaveChanges();
            return Ok("Silme işlemi tamamlandı");

        }
    }
}
