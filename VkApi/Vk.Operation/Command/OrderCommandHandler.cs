using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;

namespace Vk.Operation.Command;

public class OrderCommandHandler:
    IRequestHandler<CreateOrderCommand, ApiResponse>,
    IRequestHandler<UpdateOrderCommand, ApiResponse>,
    IRequestHandler<DeleteOrderCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public OrderCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.OrderRepository.GetAsQueryable()
           .SingleOrDefaultAsync(x => x.OrderNumber == request.Model.OrderNumber ,cancellationToken);
        if (entity is not null)
        {
            return new ApiResponse("Error");
        }
        Order mapped = mapper.Map<Order>(request.Model);
        
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