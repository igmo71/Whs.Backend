using AutoMapper;
using Whs.Application.Common.Mappings;
using Whs.Domain;

namespace Whs.Application.Services.Products.Queries.GetProductList
{
    public class ProductLookupDto : IMapWith<Product>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductLookupDto>()
                .ForMember(productDto => productDto.Id, opt => opt.MapFrom(product => product.Id))
                .ForMember(productDto => productDto.Name, opt => opt.MapFrom(product => product.Name));
        }
    }
}
