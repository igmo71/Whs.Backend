using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whs.Application.Common.Mappings;
using Whs.Domain;

namespace Whs.Application.Services.Products.Queries.GetProductDetails
{
    public class ProductDetailsVm : IMapWith<Product>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetailsVm>()
                .ForMember(productVm => productVm.Id, opt => opt.MapFrom(p => p.Id))
                .ForMember(productVm => productVm.Name, opt => opt.MapFrom(p => p.Name))
                .ForMember(productVm => productVm.Description, opt => opt.MapFrom(p => p.Description));
        }
    }
}
