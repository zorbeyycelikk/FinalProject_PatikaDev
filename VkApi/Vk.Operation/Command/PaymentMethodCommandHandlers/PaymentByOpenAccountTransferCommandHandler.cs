using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class PaymentByOpenAccountTransferCommandHandler:
    IRequestHandler<CreatePaymentOpenAccountTransferCommand, ApiResponse<PaymentByOpenAccountResponse>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public PaymentByOpenAccountTransferCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<PaymentByOpenAccountResponse>> Handle(CreatePaymentOpenAccountTransferCommand request, CancellationToken cancellationToken)
    {

        var x = await unitOfWork.CustomerRepository.GetById(request.Model.CustomerId , cancellationToken);

        if (x.openAccountLimit < request.Model.Amount)
        {
            return new ApiResponse<PaymentByOpenAccountResponse>("Yetersiz Bakiye");
        }
        
        x.openAccountLimit = x.openAccountLimit - request.Model.Amount;
        
        var refNumber = Guid.NewGuid().ToString().Replace("-", "").ToLower();
        var status = "Successful";
        
        OpenAccountTransaction senderTransaction = new OpenAccountTransaction();
        senderTransaction.Id = senderTransaction.MakeId(senderTransaction.Id);
        senderTransaction.CustomerId = request.Model.CustomerId;
        senderTransaction.ReceiverCustomerId = request.Model.ReceiverCustomerId;
        senderTransaction.refNumber = refNumber;
        senderTransaction.PaymentMethod = "Open Account";
        senderTransaction.Who = "Sender";
        senderTransaction.Amount = request.Model.Amount;
        senderTransaction.Status = status;
        senderTransaction.TransactionDate = DateTime.UtcNow;

        await unitOfWork.OpenAccountTransactionRepository.AddAsync(senderTransaction,cancellationToken);
        unitOfWork.Save();

        var response = mapper.Map<PaymentByOpenAccountResponse>(request.Model);
        response.Status = status;
        response.Who = "Sender";
        response.PaymentMethod = "Open Account";
        response.refNumber = refNumber;
        response.TransactionDate = DateTime.UtcNow;
        
        return new ApiResponse<PaymentByOpenAccountResponse>(response);
    }
}