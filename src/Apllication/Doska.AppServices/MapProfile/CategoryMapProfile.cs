using AutoMapper;
using Doska.Contracts.CategoryDto;
using Doska.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.MapProfile
{
    public class CategoryMapProfile : Profile
    {
        public CategoryMapProfile()
        {
            CreateMap<Category, InfoCategoryResponse>().ReverseMap();
            CreateMap<Category, CreateOrEditCategoryRequest>().ReverseMap();
        }
    }
}
