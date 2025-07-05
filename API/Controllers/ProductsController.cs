using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")] //https://localhost:5001/api/products
    [ApiController]
    public class ProductsController (StoreContext context) : ControllerBase
    {         
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await context.Products.ToListAsync();

            if (!products.Any())return NoContent();
            
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) 
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();
            
            return Ok(product);
        }
    }
}
