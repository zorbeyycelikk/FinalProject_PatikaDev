using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class AccountCommandHandler:
    IRequestHandler<CreateAccountCommand, ApiResponse>,
    IRequestHandler<UpdateAccountCommand, ApiResponse>,
    IRequestHandler<DeleteAccountCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public AccountCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.AccountRepository.GetAsQueryable()
            .Where(x => x.AccountNumber == request.Model.AccountNumber)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (entity is not null)
        {
            return new ApiResponse("Error");
        }
        
        Account mapped = mapper.Map<Account>(request.Model);
        mapped.Id = mapped.MakeId(mapped.Id);
        
        unitOfWork.AccountRepository.AddAsync(mapped,cancellationToken);
        unitOfWork.Save();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.AccountRepository.GetById(request.Id, cancellationToken);
        
        if (entity is null)
        {
            return new ApiResponse("Error");
        }

        entity.Name = request.Model.Name;
        
        unitOfWork.AccountRepository.Update(entity);
        unitOfWork.Save();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.AccountRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        unitOfWork.AccountRepository.Remove(request.Id);
        unitOfWork.Save();
        
        return new ApiResponse();
    }
}