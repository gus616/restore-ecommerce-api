using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")] //https://localhost:5001/api/products
    [ApiController]
    public class ProductsController (StoreContext context) : ControllerBase
    {         
        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            var products = context.Products.ToList();

            if (!products.Any())return NoContent();
            
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id) 
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null) return NotFound();
            
            return Ok(product);
        }
    }
}
