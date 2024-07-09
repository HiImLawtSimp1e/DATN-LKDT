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
    public class ApplicationRoleProfile : Profile
    {
        public ApplicationRoleProfile() 
        {
            //Mapping DTOs to Entity
            CreateMap<AddApplicationRoleDto, ApplicationRole>();
            CreateMap<UpdateApplicationRoleDto, ApplicationRole>();
        }
    }
}
