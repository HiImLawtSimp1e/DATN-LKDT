using AutoMapper;
using shop.Application.ViewModels.RequestDTOs;
using shop.Application.ViewModels.ResponseDTOs;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile() 
        {
            //Mapping DTOs to Entity
            CreateMap<AddAccountDto, AccountEntity>();
            CreateMap<UpdateAccountDto, AccountEntity>();
        }
    }
}
