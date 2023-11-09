using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

public class SessionCustomerQueryHandler :
    IRequestHandler<GetSessionCustomerInfoByCustomerNumber, ApiResponse<CustomerResponse>>,
    IRequestHandler<GetSessionCustomerAllAccountInfoByCustomerNumber, ApiResponse<List<AccountResponse>>>,
    IRequestHandler<GetSessionCustomerAllCardInfoByCustomerNumber, ApiResponse<List<CardResponse>>>,
    IRequestHandler<GetSessionCustomerAllBasketInfoByCustomerNumber, ApiResponse<List<BasketResponse>>>,
    IRequestHandler<GetSessionCustomerActiveBasketInfoByCustomerNumber, ApiResponse<BasketResponse>>,
    IRequestHandler<GetSessionCustomerAllBasketItemInfoByCustomerNumber, ApiResponse<List<BasketItemResponse>>>,
    IRequestHandler<GetSessionCustomerBasketItemInfoForActiveBasketByCustomerNumber, ApiResponse<List<BasketItemResponse>>>,
    IRequestHandler<GetSessionCustomerAllOrderInfoByCustomerNumber, ApiResponse<List<OrderResponse>>>,
    IRequestHandler<GetSessionCustomerProductListInfoByCustomerNumber, ApiResponse<List<ProductResponse>>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private IRequestHandler<GetSessionCustomerAllBasketItemInfoByCustomerNumber, ApiResponse<List<BasketItemResponse>>>
        _requestHandlerImplementation;

    public SessionCustomerQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(GetSessionCustomerInfoByCustomerNumber request, CancellationToken cancellationToken)
    {
        Customer entity = await unitOfWork.CustomerRepository.GetById(request.CustomerNumber, cancellationToken);

        if (entity is null)
        {
            return new ApiResponse<CustomerResponse>("Error" , false);
        }
        var mapped = mapper.Map<CustomerResponse>(entity);
        return new ApiResponse<CustomerResponse>(mapped);
    }

    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetSessionCustomerAllAccountInfoByCustomerNumber request, CancellationToken cancellationToken)
    {
        List<Account> entities = await unitOfWork.AccountRepository.GetAsQueryable()
            .Where(a => a.CustomerId == request.CustomerNumber)
            .ToListAsync(cancellationToken);
        
        if (entities == null || !entities.Any())
        {
            return new ApiResponse<List<AccountResponse>>("Error" , false);
        }
        
        var mapped = mapper.Map<List<AccountResponse>>(entities);
        return new ApiResponse<List<AccountResponse>>(mapped);
    }
    
    public async Task<ApiResponse<List<CardResponse>>> Handle(GetSessionCustomerAllCardInfoByCustomerNumber request, CancellationToken cancellationToken)
    {
        List<Card> entities = await unitOfWork.CardRepository.GetAsQueryable()
            .Where(a => a.Account.CustomerId == request.CustomerNumber)
            .ToListAsync(cancellationToken);
        
        if (entities == null || !entities.Any())
        {
            return new ApiResponse<List<CardResponse>>("Error" , false);
        }
        
        var mapped = mapper.Map<List<CardResponse>>(entities);
        return new ApiResponse<List<CardResponse>>(mapped);    
    }

    public async Task<ApiResponse<List<BasketResponse>>> Handle(GetSessionCustomerAllBasketInfoByCustomerNumber request, CancellationToken cancellationToken)
    {
        List<Basket> entities = await unitOfWork.BasketRepository.GetAsQueryable("BasketItems")
            .Where(a => a.CustomerId == request.CustomerNumber)
            .ToListAsync(cancellationToken);
        
        if (entities == null || !entities.Any())
        {
            return new ApiResponse<List<BasketResponse>>("Error" , false);
        }
        
        var mapped = mapper.Map<List<BasketResponse>>(entities);
        return new ApiResponse<List<BasketResponse>>(mapped);
    }
    
    public async Task<ApiResponse<BasketResponse>> Handle(GetSessionCustomerActiveBasketInfoByCustomerNumber request, CancellationToken cancellationToken)
    {
        Basket entity = await unitOfWork.BasketRepository.GetAsQueryable("BasketItems")
            .Where(a => a.CustomerId == request.CustomerNumber && a.IsActive == true)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (entity is null)
        {
            return new ApiResponse<BasketResponse>("Error" , false);
        }
        
        var mapped = mapper.Map<BasketResponse>(entity);
        return new ApiResponse<BasketResponse>(mapped);
    }

    public async Task<ApiResponse<List<BasketItemResponse>>> Handle(GetSessionCustomerAllBasketItemInfoByCustomerNumber request, CancellationToken cancellationToken)
    {
        List<BasketItem> entities = await unitOfWork.BasketItemRepository.GetAsQueryable("Basket" , "Product")
            .Where(a => a.Basket.CustomerId == request.CustomerNumber)
            .ToListAsync(cancellationToken);
        
        if (entities == null || !entities.Any())
        {
            return new ApiResponse<List<BasketItemResponse>>("Error" , false);
        }
        
        var mapped = mapper.Map<List<BasketItemResponse>>(entities);
        return new ApiResponse<List<BasketItemResponse>>(mapped);
    }
    
    public async Task<ApiResponse<List<BasketItemResponse>>> Handle(GetSessionCustomerBasketItemInfoForActiveBasketByCustomerNumber request, CancellationToken cancellationToken)
    {
        List<BasketItem> entities = await unitOfWork.BasketItemRepository.GetAsQueryable("Basket" , "Product")
            .Where(a => a.Basket.CustomerId == request.CustomerNumber && a.Basket.IsActive == true)
            .ToListAsync(cancellationToken);
        
        if (entities == null || !entities.Any())
        {
            return new ApiResponse<List<BasketItemResponse>>("Error" , false);
        }
        
        var mapped = mapper.Map<List<BasketItemResponse>>(entities);
        return new ApiResponse<List<BasketItemResponse>>(mapped);
    }

    public async Task<ApiResponse<List<OrderResponse>>> Handle(GetSessionCustomerAllOrderInfoByCustomerNumber request, CancellationToken cancellationToken)
    {
        List<Order> entities = await unitOfWork.OrderRepository.GetAsQueryable("Basket")
            .Where(a => a.Basket.CustomerId == request.CustomerNumber)
            .ToListAsync(cancellationToken);
        
        if (entities == null || !entities.Any())
        {
            return new ApiResponse<List<OrderResponse>>("Error" , false);
        }
        
        var mapped = mapper.Map<List<OrderResponse>>(entities);
        return new ApiResponse<List<OrderResponse>>(mapped);
    }

    public async Task<ApiResponse<List<ProductResponse>>> Handle(GetSessionCustomerProductListInfoByCustomerNumber request, CancellationToken cancellationToken)
    {
        List<Product> entities = await unitOfWork.ProductRepository.GetAll(cancellationToken);
        Customer entity = await unitOfWork.CustomerRepository.GetById(request.CustomerNumber,cancellationToken);
        
        if (entities == null || !entities.Any())
        {
            return new ApiResponse<List<ProductResponse>>("Error" , false);
        }

        foreach (var x in entities)
        {
            x.Price = ( (entity.Profit * x.Price) / 100 ) + x.Price;
        }
        
        var mapped = mapper.Map<List<ProductResponse>>(entities);
        return new ApiResponse<List<ProductResponse>>(mapped);
    }
}