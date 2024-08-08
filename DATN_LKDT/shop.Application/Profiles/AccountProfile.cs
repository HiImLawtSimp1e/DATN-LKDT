using AutoMapper;
using shop.Application.ViewModels.RequestDTOs.AccountDto;
using shop.Application.ViewModels.ResponseDTOs;
using shop.Application.ViewModels.ResponseDTOs.AccountResponseDto;
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
            // Mapping list Account to DTO
            CreateMap<AccountEntity, AccountListResponseDto>();

            //Mapping Account Entity to DTO
            CreateMap<AccountEntity, AccountDetailResponseDto>()
              .ForMember(dest => dest.Name, opt => opt.Ignore())
              .ForMember(dest => dest.Email, opt => opt.Ignore())
              .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
              .ForMember(dest => dest.Address, opt => opt.Ignore());

            // Mapping Address Entity to DTO
            CreateMap<AddressEntity, AccountDetailResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Username, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore());
        }
    }
}
