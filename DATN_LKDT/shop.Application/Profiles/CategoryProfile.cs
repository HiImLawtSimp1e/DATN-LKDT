using AutoMapper;
using shop.Application.ViewModels.RequestDTOs.CategoryDto;
using shop.Application.ViewModels.ResponseDTOs.CustomerResponseDto;
using shop.Application.ViewModels.ResponseDTOs;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            //Map Entity to DTOs
            CreateMap<Category, CustomerCategoryResponseDto>();
            CreateMap<Category, CategorySelectResponseDto>();
            //Map DTOs to Entity
            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
