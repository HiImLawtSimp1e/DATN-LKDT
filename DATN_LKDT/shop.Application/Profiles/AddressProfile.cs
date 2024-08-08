using AutoMapper;
using shop.Application.ViewModels.RequestDTOs.AddressDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<CreateAddressDto, AddressEntity>();
            CreateMap<UpdateAddressDto, AddressEntity>();
        }
    }
}
