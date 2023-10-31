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
    IRequestHandler<GetBasketItemById, ApiResponse<BasketItemResponse>>
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
            return new ApiResponse<BasketItemResponse>("Error");
        }
        
        BasketItemResponse response = mapper.Map<BasketItemResponse>(x);
        return new ApiResponse<BasketItemResponse>(response);
    }
}