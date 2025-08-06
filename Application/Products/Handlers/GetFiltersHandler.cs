using Application.Products.DTOs;
using Application.Products.Interfaces;
using Application.Products.Queries;
using AutoMapper;
using MediatR;


namespace Application.Products.Handlers
{
    public class GetFiltersHandler : IRequestHandler<GetFiltersQuery, FilterDto>
    {
        private readonly IProductRepository _repository;

        public GetFiltersHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<FilterDto> Handle(GetFiltersQuery request, CancellationToken cancellationToken)
        {
            var brands = await _repository.GetDistinctBrandsAsync();
            var types = await _repository.GetDistinctTypesAsync();
            var minPrice = await _repository.GetMinPriceAsync();
            var maxPrice = await _repository.GetMaxPriceAsync();

            return new FilterDto
            {
                Brands = brands,
                Types = types,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };
        }
    }
}
