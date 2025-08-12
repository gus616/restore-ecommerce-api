using Application.Common.Models;
using Application.Products.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Queries
{
    public record GetProductListQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedResult<ProductDto>>;
}
