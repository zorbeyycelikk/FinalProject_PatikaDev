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
            .ForMember(x => x.CustomerNumber, opt => opt
                .MapFrom(src => src.Basket.CustomerNumber));
        
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
        
        CreateMap<CreateBasketRequest, Basket>();
        CreateMap<Basket, BasketResponse>()
            .ForMember(x => x.BasketItems, opt => opt
                .MapFrom(src => src.BasketItems));
        
        CreateMap<CreateBasketItemRequest, BasketItem>();
        CreateMap<BasketItem, BasketItemResponse>()
            .ForMember(x => x.CustomerNumber, opt => opt
                .MapFrom(src => src.Basket.CustomerNumber))
            .ForMember(x => x.ProductName, opt => opt
                .MapFrom(src => src.Product.Name));

        // CreateMap<CreateOrderProductRequest, OrderProduct>();
        // CreateMap<OrderProduct, OrderProductResponse>()
        //     .ForMember(x => x.CustomerNumberWhoCreateOrder, opt => opt
        //         .MapFrom(src => src.Order.CustomerNumber))
        //     .ForMember(x => x.ProductNameWhichCreatedInOrder, opt => opt
        //         .MapFrom(src => src.Product.Name));
    }
}