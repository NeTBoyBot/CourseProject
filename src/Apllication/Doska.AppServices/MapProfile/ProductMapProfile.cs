using AutoMapper;
using Doska.Contracts.ProductDto;
using Doska.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.MapProfile
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateMap<Product, InfoProductResponse>().ReverseMap();
            CreateMap<Product, CreateOrEditProductRequest>().ReverseMap();
        }
    }
}
