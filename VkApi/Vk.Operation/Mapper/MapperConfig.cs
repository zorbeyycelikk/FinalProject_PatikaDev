using AutoMapper;
using Vk.Data;
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
            .ForMember(x => x.BasketItems, opt => opt
                .MapFrom(src => src.Basket.BasketItems))
            .ForMember(x => x.CustomerId, opt => opt
                .MapFrom(src => src.Basket.CustomerId));
        
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();
        CreateMap<Product, ProductResponse>();
        
        CreateMap<CreateCardRequest, Card>();
        CreateMap<UpdateCardRequest, Card>();
        CreateMap<Card, CardResponse>()
            .ForMember(x => x.CardHolderId, opt => opt
                .MapFrom(src => src.Account.CustomerId));
        
        CreateMap<CreateAccountRequest, Account>();
        CreateMap<UpdateAccountRequest, Account>();
        CreateMap<Account, AccountResponse>();
        
        CreateMap<CreateBasketRequest, Basket>();
        CreateMap<Basket, BasketResponse>()
            .ForMember(x => x.BasketItems, opt => opt
                .MapFrom(src => src.BasketItems));
        
        CreateMap<CreateBasketItemRequest, BasketItem>();
        CreateMap<BasketItem, BasketItemResponse>()
            .ForMember(x => x.ProductName, opt => opt
                .MapFrom(src => src.Product.Name));
    }
}