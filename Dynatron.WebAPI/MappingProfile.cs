using AutoMapper;
using Dynatron.Application.Entities;
using Dynatron.WebAPI.Models;

namespace Dynatron.WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerEntity, CustomerModel>();
        }
    }
}
