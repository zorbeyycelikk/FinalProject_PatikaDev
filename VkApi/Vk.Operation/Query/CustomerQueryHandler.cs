using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;


// public record GetAllCustomerQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;
// public record GetCustomerById(string Id) : IRequest<ApiResponse<CustomerResponse>>;

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
       var x = await unitOfWork.CustomerRepository.GetAll().ToListAsync();
       List<CustomerResponse> response = mapper.Map<List<CustomerResponse>>(x);
       return new ApiResponse<List<CustomerResponse>>(response);
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerById request, CancellationToken cancellationToken)
    {
        var x = await unitOfWork.CustomerRepository.GetByIdAsync(request.Id);
        if (x is null)
        {
            return new ApiResponse<CustomerResponse>("Error");
        }
        CustomerResponse response = mapper.Map<CustomerResponse>(x);
        return new ApiResponse<CustomerResponse>(response);
    }
}