// using AutoMapper;
// using MediatR;
// using Microsoft.EntityFrameworkCore;
// using Vk.Base.Response;
// using Vk.Data;
// using Vk.Data.Domain;
// using Vk.Data.Uow;
// using Vk.Operation.Cqrs;
// using Vk.Schema;
//
// namespace Vk.Operation.Command;
//
// public class OrderProductCommandHandler:
//     IRequestHandler<CreateOrderProductCommand, ApiResponse>,
//     IRequestHandler<DeleteOrderProductCommand, ApiResponse>
// {
//     private readonly IMapper mapper;
//     private readonly IUnitOfWork unitOfWork;
//     
//     public OrderProductCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
//     {
//         this.unitOfWork = unitOfWork;
//         this.mapper = mapper;
//     }
//
//     public async Task<ApiResponse> Handle(CreateOrderProductCommand request, CancellationToken cancellationToken)
//     {
//         var entity = await unitOfWork.OrderProductRepository.GetAsQueryable()
//            .SingleOrDefaultAsync(x => x.Order.OrderNumber == request.Model.OrderNumber ,cancellationToken);
//         if (entity is not null)
//         {
//             return new ApiResponse("Error");
//         }
//         OrderProduct mapped = mapper.Map<OrderProduct>(request.Model);
//         
//         unitOfWork.OrderProductRepository.AddAsync(mapped,cancellationToken);
//         unitOfWork.Save();
//         
//         return new ApiResponse();
//     }
//     public async Task<ApiResponse> Handle(DeleteOrderProductCommand request, CancellationToken cancellationToken)
//     {
//         var entity = await unitOfWork.OrderProductRepository.GetById(request.Id, cancellationToken);
//         if (entity is null)
//         {
//             return new ApiResponse("Error");
//         }
//         unitOfWork.OrderProductRepository.Remove(request.Id);
//         unitOfWork.Save();
//         
//         return new ApiResponse();
//     }
// }