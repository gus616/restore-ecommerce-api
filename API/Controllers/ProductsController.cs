using Application.Products.Queries;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")] //https://localhost:5001/api/products
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetProductListQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
            var result = await _mediator.Send(new GetFiltersQuery());

            if(result == null)
                return NotFound();

            return Ok(result);
        }

        /*        
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
        
        */
    }
}
