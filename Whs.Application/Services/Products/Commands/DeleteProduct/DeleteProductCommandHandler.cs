using MediatR;
using Whs.Application.Common.Exceptions;
using Whs.Application.Interfaces;
using Whs.Domain;

namespace Whs.Application.Services.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IWhsDbContext _dbContext;

        public DeleteProductCommandHandler(IWhsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _dbContext.Products.FindAsync(new object[] { request.Id }, cancellationToken);

            if (product == null || product.Id != request.Id)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
