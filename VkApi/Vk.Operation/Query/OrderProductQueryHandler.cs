// using Vk.Base.Response;
// using Vk.Data;
// using Vk.Data.Uow;
// using Vk.Operation.Cqrs;
// using Vk.Schema;
//
// namespace Vk.Operation.Query;
//
// using AutoMapper;
// using MediatR;
//
// public class OrderProductQueryHandler :
//     IRequestHandler<GetAllOrderProductQuery, ApiResponse<List<OrderProductResponse>>>,
//     IRequestHandler<GetOrderProductById, ApiResponse<OrderProductResponse>>
// {
//     private readonly IMapper mapper;
//     private readonly IUnitOfWork unitOfWork;
//     
//     public OrderProductQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
//     {
//         this.unitOfWork = unitOfWork;
//         this.mapper = mapper;
//     }
//     
//     public async Task<ApiResponse<List<OrderProductResponse>>> Handle(GetAllOrderProductQuery request, CancellationToken cancellationToken)
//     {
//         List<OrderProduct> x =  await unitOfWork.OrderProductRepository.GetAll(cancellationToken , "Order" , "Product");
//         List<OrderProductResponse> response = mapper.Map<List<OrderProductResponse>>(x);
//         return new ApiResponse<List<OrderProductResponse>>(response);
//     }
//
//     public async Task<ApiResponse<OrderProductResponse>> Handle(GetOrderProductById request, CancellationToken cancellationToken)
//     {
//         OrderProduct x = await unitOfWork.OrderProductRepository.GetById(request.Id, cancellationToken , "Order" , "Product");
//         
//         if (x is null)
//         {
//             return new ApiResponse<OrderProductResponse>("Error");
//         }
//         
//         OrderProductResponse response = mapper.Map<OrderProductResponse>(x);
//         return new ApiResponse<OrderProductResponse>(response);
//     }
// }