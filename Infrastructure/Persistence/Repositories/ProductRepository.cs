using Application.Products.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Where(p => p.PictureUrl.StartsWith("https"))
                .ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products
                 .Include(p => p.Images)
                 .FirstAsync(p => p.Id == id);
        }

        public async Task<List<string>> GetDistinctBrandsAsync() => 
            await _context.Products.Select(p => p.Brand).Distinct().ToListAsync();
        

        public async Task<List<string>> GetDistinctTypesAsync() =>
            await _context.Products.Select(p => p.Type).Distinct().ToListAsync();

        public async Task<decimal> GetMinPriceAsync() =>
            await _context.Products.MinAsync(p => p.Price);

        public async Task<decimal> GetMaxPriceAsync() => 
            await _context.Products.MaxAsync(p => p.Price);
    }
}
