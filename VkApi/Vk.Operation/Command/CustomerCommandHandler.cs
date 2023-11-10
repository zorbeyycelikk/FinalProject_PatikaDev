using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Encryption;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;

namespace Vk.Operation.Command;

public class CustomerCommandHandler:
    IRequestHandler<CreateCustomerCommand, ApiResponse>,
    IRequestHandler<UpdateCustomerCommand, ApiResponse>,
    IRequestHandler<DeleteCustomerCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public CustomerCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CustomerRepository.GetAsQueryable()
            .Where(x => x.Email == request.Model.Email || x.Phone == request.Model.Phone)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity is not null)
        {
            return new ApiResponse("Error");
        }
        
        Customer mapped = mapper.Map<Customer>(request.Model);
        
        mapped.Id = mapped.MakeId(mapped.Id);
        mapped.Password = Md5.Create(mapped.Password.ToUpper());
        
        unitOfWork.CustomerRepository.AddAsync(mapped,cancellationToken);
        unitOfWork.Save();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CustomerRepository.GetById(request.Id, cancellationToken);
        
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        
        entity.Name = request.Model.Name;
        entity.Email = request.Model.Email;
        entity.Phone = request.Model.Phone;
        entity.Password = Md5.Create(request.Model.Password.ToUpper());
        entity.Profit = request.Model.Profit;
        
        unitOfWork.CustomerRepository.Update(entity);
        unitOfWork.Save();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CustomerRepository.GetById(request.Id, cancellationToken);
        
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        unitOfWork.CustomerRepository.Remove(request.Id);
        unitOfWork.Save();
        
        return new ApiResponse();
    }
}