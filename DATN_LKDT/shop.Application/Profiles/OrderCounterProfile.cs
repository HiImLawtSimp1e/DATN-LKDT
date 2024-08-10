using AutoMapper;
using shop.Application.ViewModels.RequestDTOs.OrderCounterDto;
using shop.Application.ViewModels.ResponseDTOs.OrderCounterDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Profiles
{
    public class OrderCounterProfile : Profile
    {
        public OrderCounterProfile() 
        {
            //Map Entity to DTO
            CreateMap<AddressEntity, SearchAddressItemResponse>();
            //Map DTO to Entity
            CreateMap<OrderCounterItemDto, OrderItem>();
        }
    }
}
