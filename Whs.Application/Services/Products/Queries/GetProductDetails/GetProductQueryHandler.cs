using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Whs.Application.Common.Exceptions;
using Whs.Application.Interfaces;
using Whs.Domain;

namespace Whs.Application.Services.Products.Queries.GetProductDetails
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDetailsVm>
    {
        private readonly IWhsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IWhsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductDetailsVm> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null || product.Id != request.Id)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            var result = _mapper.Map<ProductDetailsVm>(product);
            return result;
        }
    }
}
