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
                .MapFrom(src => src.Basket.BasketItems));
        
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();
        CreateMap<Product, ProductResponse>();
        
        CreateMap<CreateCardRequest, Card>();
        CreateMap<UpdateCardRequest, Card>();
        CreateMap<Card, CardResponse>()
            .ForMember(x => x.CardHolderId, opt => opt
                .MapFrom(src => src.Account.CustomerId))
            .ForMember(x => x.AccountNumber, opt => opt
                .MapFrom(src => src.Account.AccountNumber));
        
        CreateMap<CreateCardTransactionRequest, CardTransaction>();
        CreateMap<UpdateCardTransactionRequest, CardTransaction>();
        CreateMap<CardTransaction, CardTransactionResponse>();
        
        CreateMap<CreateAccountRequest, Account>();
        CreateMap<UpdateAccountRequest, Account>();
        CreateMap<Account, AccountResponse>();
        
        CreateMap<CreateBasketRequest, Basket>();
        CreateMap<Basket, BasketResponse>();
        
        CreateMap<CreateBasketItemRequest, BasketItem>();
        CreateMap<BasketItem, BasketItemResponse>()
            .ForMember(x => x.Product, opt => opt
                .MapFrom(src => src.Product));
        
        CreateMap<CreatePaymentByCardRequest, PaymentByCardResponse>();
        
        CreateMap<CreatePaymentByEftRequest, PaymentByEftResponse>();
        CreateMap<CreatePaymentByHavaleRequest, PaymentByHavaleResponse>();
        CreateMap<CreatePaymentByOpenAccountRequest, PaymentByOpenAccountResponse>();

        // CreateMap<CreateCompleteOrderWithHavaleRequest, CreateOrderRequest>()
        //     .ForSourceMember(src => src.Name, opt => opt.DoNotValidate())
        //     .ForSourceMember(src => src.AccountNumber, opt => opt.DoNotValidate())
        //     .ForSourceMember(src => src.SenderAccountNumber, opt => opt.DoNotValidate());

    //     CreateMap<CreateCompleteOrderWithHavaleRequest, CreatePaymentByHavaleRequest>()
    //         .ForSourceMember(src => src.CustomerId, opt => opt.DoNotValidate())
    //         .ForSourceMember(src => src.Address, opt => opt.DoNotValidate())
    //         .ForSourceMember(src => src.BasketId, opt => opt.DoNotValidate())
    //         .ForSourceMember(src => src.PaymentMethod, opt => opt.DoNotValidate());
    //
    }
}