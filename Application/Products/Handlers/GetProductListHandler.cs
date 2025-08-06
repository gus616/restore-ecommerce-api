using Application.Products.DTOs;
using Application.Products.Interfaces;
using Application.Products.Queries;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Handlers
{
    public class GetProductListHandler : IRequestHandler<GetProductListQuery, List<ProductDto>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public GetProductListHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<ProductDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetProductsAsync();

            if (products == null)
            {
                return null;
            }

            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
