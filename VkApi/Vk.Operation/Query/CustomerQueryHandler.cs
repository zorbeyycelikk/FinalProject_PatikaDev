using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

using AutoMapper;
using MediatR;

public class CustomerQueryHandler :
    IRequestHandler<GetAllCustomerQuery, ApiResponse<List<CustomerResponse>>>,
    IRequestHandler<GetCustomerById, ApiResponse<CustomerResponse>>
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
        List<Customer> x =  await unitOfWork.CustomerRepository.GetAll(cancellationToken, "Orders" , "Accounts");
        List<CustomerResponse> response = mapper.Map<List<CustomerResponse>>(x);
        return new ApiResponse<List<CustomerResponse>>(response);
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerById request, CancellationToken cancellationToken)
    {
        Customer x = await unitOfWork.CustomerRepository.GetById(request.Id, cancellationToken , "Orders" , "Accounts");
        
        if (x is null)
        {
            return new ApiResponse<CustomerResponse>("Error");
        }
        
        CustomerResponse response = mapper.Map<CustomerResponse>(x);
        return new ApiResponse<CustomerResponse>(response);
    }
}