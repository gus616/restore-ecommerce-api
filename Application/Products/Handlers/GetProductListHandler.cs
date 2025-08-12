using Application.Common.Models;
using Application.Products.DTOs;
using Application.Products.Interfaces;
using Application.Products.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Handlers
{
    public class GetProductListHandler : IRequestHandler<GetProductListQuery, PaginatedResult<ProductDto>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public GetProductListHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<ProductDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll();

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                 .OrderBy(p => p.Name) // stable ordering
                 .Skip((request.PageNumber - 1) * request.PageSize)
                 .Take(request.PageSize)
                 .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);
                

           return new PaginatedResult<ProductDto>(items, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
