using AutoMapper;
using Vk.Data.Domain;
using Vk.Schema;

namespace Vk.Operation.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<CreateCustomerRequest, Customer>();
        CreateMap<UpdateCustomerRequest, Customer>();
        CreateMap<Customer, CustomerResponse>();
        
        CreateMap<CreateOrderRequest, Order>();
        CreateMap<UpdateOrderRequest, Order>();
        CreateMap<Order, OrderResponse>()
            .ForMember(x => x.CustomerName, opt => opt
                .MapFrom(src => src.Customer.Name));
        
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();
        CreateMap<Product, ProductResponse>();
        
        CreateMap<CreateCardRequest, Card>();
        CreateMap<UpdateCardRequest, Card>();
        CreateMap<Card, CardResponse>()
            .ForMember(x => x.CardHolderNumber, opt => opt
                .MapFrom(src => src.Account.CustomerNumber));
        
        CreateMap<CreateAccountRequest, Account>();
        CreateMap<UpdateAccountRequest, Account>();
        CreateMap<Account, AccountResponse>();
    }
}