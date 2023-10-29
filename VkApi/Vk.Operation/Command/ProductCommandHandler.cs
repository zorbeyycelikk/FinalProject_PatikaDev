using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;

namespace Vk.Operation.Command;

public class ProductCommandHandler:
    IRequestHandler<CreateProductCommand, ApiResponse>,
    IRequestHandler<UpdateProductCommand, ApiResponse>,
    IRequestHandler<DeleteProductCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public ProductCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.ProductRepository.GetAsQueryable()
           .SingleOrDefaultAsync(x => x.ProductNumber == request.Model.ProductNumber ,cancellationToken);
        if (entity is not null)
        {
            return new ApiResponse("Error");
        }
        Product mapped = mapper.Map<Product>(request.Model);
        
        unitOfWork.ProductRepository.AddAsync(mapped,cancellationToken);
        unitOfWork.Save();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.ProductRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }

        entity.Name = request.Model.Name;
        entity.Price = request.Model.Price;
        entity.Stock = request.Model.Stock;
        
        unitOfWork.ProductRepository.Update(entity);
        unitOfWork.Save();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.ProductRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        unitOfWork.ProductRepository.Remove(request.Id);
        unitOfWork.Save();
        
        return new ApiResponse();
    }
}