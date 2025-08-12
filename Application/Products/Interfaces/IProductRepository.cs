using Domain.Entities;

namespace Application.Products.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        IQueryable<Product> GetAll();
        Task<Product> GetProductById(int id);
        Task<List<string>> GetDistinctBrandsAsync();
        Task<List<string>> GetDistinctTypesAsync();
        Task<decimal> GetMinPriceAsync();
        Task<decimal> GetMaxPriceAsync();
    }
}
