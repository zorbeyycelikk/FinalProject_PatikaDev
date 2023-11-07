using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Encryption;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class PaymentByCardCommandHandler:
    IRequestHandler<CreatePaymentCardTransferCommand, ApiResponse<PaymentByCardResponse>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public PaymentByCardCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<PaymentByCardResponse>> Handle(CreatePaymentCardTransferCommand request, CancellationToken cancellationToken)
    {

        // Böyle numarada bir alıcı var mı ?
        var account = await unitOfWork.AccountRepository.GetAsQueryable()
            .Where(x => x.AccountNumber == request.Model.receiverAccountNumber && x.IsActive == true)
            .SingleOrDefaultAsync(cancellationToken);

        if (account is null)
        {
            return new ApiResponse<PaymentByCardResponse>("Error", false);
        }
        
        
        
        //
        
        
        
        
        return new ApiResponse<PaymentByCardResponse>("true"); // değişecek
    }
    
}