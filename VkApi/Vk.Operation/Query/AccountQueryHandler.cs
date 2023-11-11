using System.Data.Entity;
using LinqKit;
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
    IRequestHandler<GetAccountById, ApiResponse<AccountResponse>>,
    IRequestHandler<GetAccountByParametersQuery, ApiResponse<List<AccountResponse>>>
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

    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAccountByParametersQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Account>(true);

        if (!string.IsNullOrWhiteSpace(request.Name))
            predicate.And(x => x.Name == request.Name);
        if (!string.IsNullOrWhiteSpace(request.AccountNumber))
            predicate.And(x => x.AccountNumber == request.AccountNumber);
        if (!string.IsNullOrWhiteSpace(request.IBAN))
            predicate.And(x => x.IBAN == request.IBAN);
        if (request.minBalance > 0)
            predicate.And(x => x.Balance >= request.minBalance);
        if (request.maxBalance > 0)
            predicate.And(x => x.Balance <= request.maxBalance);
        
        List<Account> accounts = await unitOfWork.AccountRepository.GetAsQueryable("Customer")
            .Where(predicate).ToListAsync(cancellationToken);
        if (!accounts.Any())
        {
            return new ApiResponse<List<AccountResponse>>("Error");
        }
        var mapped = mapper.Map<List<AccountResponse>>(accounts);
        return new ApiResponse<List<AccountResponse>>(mapped);
    }
}