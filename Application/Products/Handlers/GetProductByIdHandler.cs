using Application.Products.DTOs;
using Application.Products.Interfaces;
using Application.Products.Queries;
using AutoMapper;
using MediatR;

namespace Application.Products.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductById(request.id);

            if (product == null) 
            {
                throw new KeyNotFoundException($"Product with id {request.id} not found");
            }

            return _mapper.Map<ProductDto>(product);
        }
    }
}
