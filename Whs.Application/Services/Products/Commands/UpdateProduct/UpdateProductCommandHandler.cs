using MediatR;
using Microsoft.EntityFrameworkCore;
using Whs.Application.Common.Exceptions;
using Whs.Application.Interfaces;
using Whs.Domain;

namespace Whs.Application.Services.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IWhsDbContext _dbContext;

        public UpdateProductCommandHandler(IWhsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null || product.Id != request.Id)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            product.Name = request.Name;
            product.Description = request.Description;

            await  _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
