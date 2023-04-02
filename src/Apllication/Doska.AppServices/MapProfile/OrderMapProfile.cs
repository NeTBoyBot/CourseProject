using AutoMapper;
using Doska.Contracts.OrderDto;
using Doska.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.MapProfile
{
    public class OrderMapProfile : Profile
    {
        public OrderMapProfile()
        {
            CreateMap<Order, InfoOrderResponse>().ReverseMap();
            CreateMap<Order, CreateOrEditOrderRequest>().ReverseMap();
        }
    }
}
