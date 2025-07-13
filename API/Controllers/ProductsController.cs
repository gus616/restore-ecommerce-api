using API.Data;
using API.DTOs;
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
            var products = await context.Products.Where(p => p.PictureUrl.StartsWith("https")).ToListAsync();

            if (!products.Any())return NoContent();
            
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) 
        {
            var product = await context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            var dto = new ProductDto
            { 
                Id = id,           
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                Type = product.Type,
                Brand = product.Brand,
                QuantityInStock = product.QuantityInStock,
                Images = product.Images.Select(i => i.ImageUrl).ToList()
            };
            
            return Ok(dto);
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
            var brands = await context.Products
                .Select(p => p.Brand)
                .Distinct()
                .ToListAsync();

            var types = await context.Products.Select(p => p.Type)
                .Distinct()
                .ToListAsync();

            var minPrice = await context.Products.MinAsync(p => p.Price);
            var maxPrice = await context.Products.Where(p => p.PictureUrl.StartsWith("https")).MaxAsync(p => p.Price);

            

            var filters = new FilterDto
            {
                Brands = brands,
                Types = types,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            return Ok(filters);
        }
    }
}
