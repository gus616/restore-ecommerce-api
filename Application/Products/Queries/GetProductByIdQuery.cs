using Application.Products.DTOs;
using MediatR;

namespace Application.Products.Queries
{
    public record GetProductByIdQuery(int id): IRequest<ProductDto>;    
}
