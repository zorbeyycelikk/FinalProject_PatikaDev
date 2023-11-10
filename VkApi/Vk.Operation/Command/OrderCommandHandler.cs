using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class OrderCommandHandler:
    IRequestHandler<CreateOrderCommand, ApiResponse>,
    IRequestHandler<UpdateOrderCommand, ApiResponse>,
    IRequestHandler<DeleteOrderCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public OrderCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork
        )
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order mapped = mapper.Map<Order>(request.Model);
        var refNumber = Guid.NewGuid().ToString().Replace("-", "").ToLower();
        mapped.Id = mapped.MakeId(mapped.Id);
        mapped.Status = "Basarisiz";
        mapped.OrderNumber = refNumber;
        mapped.InsertDate = DateTime.UtcNow;

        if (request.Model.PaymentRefCode == " ")
        {
            unitOfWork.OrderRepository.AddAsync(mapped,cancellationToken);
            unitOfWork.Save();
            return new ApiResponse("Error");
        }
        
        var checkStock = await new checkStockCommand(unitOfWork).CheckStock(request.Model.BasketId, cancellationToken);
        if (!checkStock.Success)
        {
            unitOfWork.OrderRepository.AddAsync(mapped,cancellationToken);
            unitOfWork.Save();
            return new ApiResponse("Error");
        }
        
        mapped.Status = "Basarili";
        unitOfWork.OrderRepository.AddAsync(mapped,cancellationToken);
        unitOfWork.Save();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.OrderRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }

        entity.Address = request.Model.Address;
        entity.Description = request.Model.Description;
        entity.Status = request.Model.Status;
        
        unitOfWork.OrderRepository.Update(entity);
        unitOfWork.Save();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.OrderRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        unitOfWork.OrderRepository.Remove(request.Id);
        unitOfWork.Save();
        
        return new ApiResponse();
    }
}