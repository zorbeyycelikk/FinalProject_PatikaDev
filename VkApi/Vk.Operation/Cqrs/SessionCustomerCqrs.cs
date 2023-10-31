using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record GetSessionCustomerInfoByCustomerNumber(string CustomerNumber) : IRequest<ApiResponse<CustomerResponse>>;

public record GetSessionCustomerAllAccountInfoByCustomerNumber(string CustomerNumber) : IRequest<ApiResponse<List<AccountResponse>>>;

public record GetSessionCustomerAllCardInfoByCustomerNumber(string CustomerNumber) : IRequest<ApiResponse<List<CardResponse>>>;

public record GetSessionCustomerAllBasketInfoByCustomerNumber(string CustomerNumber) : IRequest<ApiResponse<List<BasketResponse>>>;

public record GetSessionCustomerAllBasketItemInfoByCustomerNumber(string CustomerNumber) : IRequest<ApiResponse<List<BasketItemResponse>>>;

public record GetSessionCustomerAllOrderInfoByCustomerNumber(string CustomerNumber) : IRequest<ApiResponse<List<OrderResponse>>>;

public record GetSessionCustomerProductListInfoByCustomerNumber(string CustomerNumber) : IRequest<ApiResponse<List<ProductResponse>>>;
