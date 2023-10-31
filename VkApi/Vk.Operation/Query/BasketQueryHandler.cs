using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

using AutoMapper;
using MediatR;

public class BasketQueryHandler :
    IRequestHandler<GetAllBasketQuery, ApiResponse<List<BasketResponse>>>,
    IRequestHandler<GetBasketById, ApiResponse<BasketResponse>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public BasketQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<List<BasketResponse>>> Handle(GetAllBasketQuery request, CancellationToken cancellationToken)
    {
        List<Basket> x =  await unitOfWork.BasketRepository.GetAll(cancellationToken,"Customer" , "Order" , "BasketItems");
        List<BasketResponse> response = mapper.Map<List<BasketResponse>>(x);
        return new ApiResponse<List<BasketResponse>>(response);
    }

    public async Task<ApiResponse<BasketResponse>> Handle(GetBasketById request, CancellationToken cancellationToken)
    {
        Basket x = await unitOfWork.BasketRepository.GetById(request.Id, cancellationToken ,"Customer" , "Order","BasketItems");
        
        if (x is null)
        {
            return new ApiResponse<BasketResponse>("Error");
        }
        
        BasketResponse response = mapper.Map<BasketResponse>(x);
        return new ApiResponse<BasketResponse>(response);
    }
}