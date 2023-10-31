using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

using AutoMapper;
using MediatR;

public class OrderQueryHandler :
    IRequestHandler<GetAllOrderQuery, ApiResponse<List<OrderResponse>>>,
    IRequestHandler<GetOrderById, ApiResponse<OrderResponse>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public OrderQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<List<OrderResponse>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        List<Order> x =  await unitOfWork.OrderRepository.GetAll(cancellationToken, "Basket");
        List<OrderResponse> response = mapper.Map<List<OrderResponse>>(x);
        return new ApiResponse<List<OrderResponse>>(response);
    }

    public async Task<ApiResponse<OrderResponse>> Handle(GetOrderById request, CancellationToken cancellationToken)
    {
        Order x = await unitOfWork.OrderRepository.GetById(request.Id, cancellationToken , "Basket");
        
        if (x is null)
        {
            return new ApiResponse<OrderResponse>("Error");
        }
        
        OrderResponse response = mapper.Map<OrderResponse>(x);
        return new ApiResponse<OrderResponse>(response);
    }
}