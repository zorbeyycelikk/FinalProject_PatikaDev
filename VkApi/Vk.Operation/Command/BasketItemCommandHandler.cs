using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;

namespace Vk.Operation.Command;

public class BasketItemCommandHandler:
    IRequestHandler<CreateBasketItemCommand, ApiResponse>,
    IRequestHandler<DeleteBasketItemCommand, ApiResponse>,
    IRequestHandler<HardDeleteBasketItemCommand, ApiResponse>,
    IRequestHandler<HardDeleteBasketItemByProductNumberCommand, ApiResponse>

{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public BasketItemCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
    {
        BasketItem mapped = mapper.Map<BasketItem>(request.Model);
        mapped.Id = mapped.MakeId(mapped.Id);
        var x = await unitOfWork.BasketItemRepository.GetAsQueryable()
            .Where(x => x.ProductId == request.Model.ProductId && x.BasketId == request.Model.BasketId)
            .SingleOrDefaultAsync(cancellationToken);
        if (x is null)
        {
            unitOfWork.BasketItemRepository.AddAsync(mapped,cancellationToken);
        }
        else
        {
            x.Quantity = x.Quantity + request.Model.Quantity;
        }
        unitOfWork.Save();
        return new ApiResponse();
    }
    
    public async Task<ApiResponse> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.BasketItemRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        unitOfWork.BasketItemRepository.Remove(request.Id);
        unitOfWork.Save();
        return new ApiResponse();
    }
    
    public async Task<ApiResponse> Handle(HardDeleteBasketItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.BasketItemRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        unitOfWork.BasketItemRepository.HardDelete(request.Id);
        unitOfWork.Save();
        return new ApiResponse();
    }
    
    public async Task<ApiResponse> Handle(HardDeleteBasketItemByProductNumberCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.BasketItemRepository.GetAsQueryable()
            .Where(x => x.Product.Id == request.Id).SingleOrDefaultAsync(cancellationToken);
        
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        unitOfWork.BasketItemRepository.HardDelete(entity.Id);
        unitOfWork.Save();
        return new ApiResponse();
    }
}