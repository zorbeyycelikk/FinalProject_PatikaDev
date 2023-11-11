using LinqKit;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

using AutoMapper;
using MediatR;

public class BasketItemQueryHandler :
    IRequestHandler<GetAllBasketItemQuery, ApiResponse<List<BasketItemResponse>>>,
    IRequestHandler<GetBasketItemById, ApiResponse<BasketItemResponse>>,
    IRequestHandler<GetBasketItemByParametersQuery, ApiResponse<List<BasketItemResponse>>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public BasketItemQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<List<BasketItemResponse>>> Handle(GetAllBasketItemQuery request, CancellationToken cancellationToken)
    {
        List<BasketItem> x =  await unitOfWork.BasketItemRepository.GetAll(cancellationToken,"Basket" , "Product");
        List<BasketItemResponse> response = mapper.Map<List<BasketItemResponse>>(x);
        return new ApiResponse<List<BasketItemResponse>>(response);
    }

    public async Task<ApiResponse<BasketItemResponse>> Handle(GetBasketItemById request, CancellationToken cancellationToken)
    {
        BasketItem x = await unitOfWork.BasketItemRepository.GetById(request.Id, cancellationToken ,"Basket" , "Product");
        
        if (x is null)
        {
            return new ApiResponse<BasketItemResponse>("Error" , false);
        }
        
        BasketItemResponse response = mapper.Map<BasketItemResponse>(x);
        return new ApiResponse<BasketItemResponse>(response);
    }

    public async Task<ApiResponse<List<BasketItemResponse>>> Handle(GetBasketItemByParametersQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<BasketItem>(true);

        if (request.minQuantity > 0)
            predicate.And(x => x.Quantity >= request.minQuantity);
        if (request.maxQuantity > 0)
            predicate.And(x => x.Quantity <= request.maxQuantity);

        List<BasketItem> BasketItems = await unitOfWork.BasketItemRepository.GetAsQueryable("Basket","Product")
            .Where(predicate).ToListAsync(cancellationToken);

        if (!BasketItems.Any())
        {
            return new ApiResponse<List<BasketItemResponse>>("Error");
        }

        var mapped = mapper.Map<List<BasketItemResponse>>(BasketItems);
        return new ApiResponse<List<BasketItemResponse>>(mapped);
    }
}