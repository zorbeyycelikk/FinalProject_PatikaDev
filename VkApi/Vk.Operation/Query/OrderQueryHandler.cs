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

public class OrderQueryHandler :
    IRequestHandler<GetAllOrderQuery, ApiResponse<List<OrderResponse>>>,
    IRequestHandler<GetOrderById, ApiResponse<OrderResponse>>,
    IRequestHandler<GetOrderByParametersQuery, ApiResponse<List<OrderResponse>>>

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
            return new ApiResponse<OrderResponse>("Error" , false);
        }
        
        OrderResponse response = mapper.Map<OrderResponse>(x);
        return new ApiResponse<OrderResponse>(response);
    }

    public async Task<ApiResponse<List<OrderResponse>>> Handle(GetOrderByParametersQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Order>(true);
        if (!string.IsNullOrWhiteSpace(request.Id))
            predicate.And(x => x.Id == request.Id);
        if (!string.IsNullOrWhiteSpace(request.CustomerId))
            predicate.And(x => x.CustomerId == request.CustomerId);
        if (!string.IsNullOrWhiteSpace(request.OrderNumber))
            predicate.And(x => x.OrderNumber == request.OrderNumber);
        if (!string.IsNullOrWhiteSpace(request.Description))
            predicate.And(x => x.Description.Contains(request.Description));
        if (!string.IsNullOrWhiteSpace(request.Address))
            predicate.And(x => x.Address.Contains(request.Address));
        if (!string.IsNullOrWhiteSpace(request.PaymentMethod))
            predicate.And(x => x.PaymentMethod == request.PaymentMethod);
        if (!string.IsNullOrWhiteSpace(request.PaymentRefCode))
            predicate.And(x => x.PaymentRefCode == request.PaymentRefCode);
        if (request.minAmount > 0)
            predicate.And(x => x.Amount >= request.minAmount);
        if (request.maxAmount > 0)
            predicate.And(x => x.Amount <= request.maxAmount);
        if (!string.IsNullOrWhiteSpace(request.Status))
            predicate.And(x => x.Status == request.Status);
        List<Order> orders = await unitOfWork.OrderRepository.GetAsQueryable()
            .Where(predicate).ToListAsync(cancellationToken);
        if (!orders.Any())
        {
            return new ApiResponse<List<OrderResponse>>("Error");
        }
        var mapped = mapper.Map<List<OrderResponse>>(orders);
        return new ApiResponse<List<OrderResponse>>(mapped);
    }
}