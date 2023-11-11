using System.Data.Entity;
using LinqKit;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;
using AutoMapper;
using MediatR;

namespace Vk.Operation.Query;

public class CustomerQueryHandler :
    IRequestHandler<GetAllCustomerQuery, ApiResponse<List<CustomerResponse>>>,
    IRequestHandler<GetCustomerById, ApiResponse<CustomerResponse>>,
    IRequestHandler<GetCustomerByParametersQuery, ApiResponse<List<CustomerResponse>>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public CustomerQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        List<Customer> x =  await unitOfWork.CustomerRepository.GetAll(cancellationToken, "Accounts");
        List<CustomerResponse> response = mapper.Map<List<CustomerResponse>>(x);
        return new ApiResponse<List<CustomerResponse>>(response);
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerById request, CancellationToken cancellationToken)
    {
        Customer x = await unitOfWork.CustomerRepository.GetById(request.Id, cancellationToken , "Accounts");
        
        if (x is null)
        {
            return new ApiResponse<CustomerResponse>("Error" , false);
        }
        
        CustomerResponse response = mapper.Map<CustomerResponse>(x);
        return new ApiResponse<CustomerResponse>(response);
    }
    
    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetCustomerByParametersQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Customer>(true);
        
        if (!string.IsNullOrWhiteSpace(request.Id))
            predicate.And(x => x.Id == request.Id);
        if (!string.IsNullOrWhiteSpace(request.Name))
            predicate.And(x => x.Name == request.Name);
        if (!string.IsNullOrWhiteSpace(request.Email))
            predicate.And(x => x.Email == request.Email);
        if (!string.IsNullOrWhiteSpace(request.Phone))
            predicate.And(x => x.Phone == request.Phone);
        if (!string.IsNullOrWhiteSpace(request.Role))
            predicate.And(x => x.Role == request.Role);
        if (request.minProfit > 0)
            predicate.And(x => x.Profit >= request.minProfit);
        if (request.maxProfit > 0)
            predicate.And(x => x.Profit <= request.maxProfit);
        if (request.minopenAccountLimit > 0)
            predicate.And(x => x.openAccountLimit >= request.minopenAccountLimit);
        if (request.maxopenAccountLimit > 0)
            predicate.And(x => x.openAccountLimit <= request.maxopenAccountLimit);
        
        List<Customer> customers = await unitOfWork.CustomerRepository.GetAsQueryable("Accounts")
            .Where(predicate).ToListAsync(cancellationToken);
        if (!customers.Any())
        {
            return new ApiResponse<List<CustomerResponse>>("Error");
        }
        var mapped = mapper.Map<List<CustomerResponse>>(customers);
        return new ApiResponse<List<CustomerResponse>>(mapped);
    }
}