using MediatR;

namespace Whs.Application.Services.Products.Queries.GetProductDetails
{
    public class GetProductQuery : IRequest<ProductDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
