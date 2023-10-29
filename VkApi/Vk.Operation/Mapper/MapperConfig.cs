using AutoMapper;
using Vk.Data.Domain;
using Vk.Schema;

namespace Vk.Operation.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<CustomerCreateRequest, Customer>();
        CreateMap<CustomerUpdateRequest, Customer>();
        CreateMap<Customer, CustomerResponse>();
    }
}