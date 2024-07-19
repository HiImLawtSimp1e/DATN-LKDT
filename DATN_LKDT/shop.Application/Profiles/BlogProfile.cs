using AutoMapper;
using shop.Application.ViewModels.RequestDTOs.BlogDto;
using shop.Application.ViewModels.ResponseDTOs.CustomerResponseDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Profiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile() 
        {
            //Mapping Entity to DTOs
            CreateMap<BlogEntity, CustomerBlogResponse>();

            //Mapping DTOs to Entity
            CreateMap<AddBlogDto, BlogEntity>();
            CreateMap<UpdateBlogDto, BlogEntity>();
        }
    }
}
