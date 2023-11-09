using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

using AutoMapper;
using MediatR;

public class AccountQueryHandler :
    IRequestHandler<GetAllAccountQuery, ApiResponse<List<AccountResponse>>>,
    IRequestHandler<GetAccountById, ApiResponse<AccountResponse>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public AccountQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAllAccountQuery request, CancellationToken cancellationToken)
    {
        List<Account> x =  await unitOfWork.AccountRepository.GetAll(cancellationToken , "Customer");
        List<AccountResponse> response = mapper.Map<List<AccountResponse>>(x);
        return new ApiResponse<List<AccountResponse>>(response);
    }

    public async Task<ApiResponse<AccountResponse>> Handle(GetAccountById request, CancellationToken cancellationToken)
    {
        Account x = await unitOfWork.AccountRepository.GetById(request.Id, cancellationToken,"Customer");
        
        if (x is null)
        {
            return new ApiResponse<AccountResponse>("Error" , false);
        }
        
        AccountResponse response = mapper.Map<AccountResponse>(x);
        return new ApiResponse<AccountResponse>(response);
    }
}