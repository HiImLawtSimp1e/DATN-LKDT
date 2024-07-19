using AutoMapper;
using shop.Application.ViewModels.RequestDTOs;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile() 
        {
            //Mapping DTOs to Entity
            CreateMap<AddApplicationUserDto, ApplicationUser>();
            CreateMap<UpdateApplicationUserDto, ApplicationUser>();
        }
    }
}
