using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class PaymentByEftTransferCommandHandler:
    IRequestHandler<CreatePaymentByEftTransferCommand, ApiResponse<PaymentByEftResponse>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public PaymentByEftTransferCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<PaymentByEftResponse>> Handle(CreatePaymentByEftTransferCommand request, CancellationToken cancellationToken)
    {
        
        // böyle bir iban numarasına sahip aktif bir hesap ve alıcı ismi ile eşleşen hesap var mı ?
        var receiverAccount  = await unitOfWork.AccountRepository.GetAsQueryable()
            .Where(x => x.IBAN == request.Model.IBAN && x.IsActive == true && x.Name == request.Model.Name)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (receiverAccount is null)
        {
            return new ApiResponse<PaymentByEftResponse>("Error", false);
        }
        
        var senderAccount = await unitOfWork.AccountRepository.GetAsQueryable()
            .Where(x => x.AccountNumber == request.Model.SenderAccountNumber).SingleOrDefaultAsync(cancellationToken);
        // Ödemek için yeterli miktarda para yoktur.
        if (senderAccount.Balance < request.Model.Amount)
        {
            return new ApiResponse<PaymentByEftResponse>("Error", false);
        }
        
        receiverAccount.Balance = receiverAccount.Balance + request.Model.Amount;
        senderAccount.Balance = senderAccount.Balance - request.Model.Amount;
        var refNumber = Guid.NewGuid().ToString().Replace("-", "").ToLower();
        var status = "Successful";
        AccountTransaction senderTransaction = new AccountTransaction();
        senderTransaction.Id = senderTransaction.MakeId(senderTransaction.Id);
        senderTransaction.AccountId = senderAccount.Id;
        senderTransaction.refNumber = refNumber;
        senderTransaction.AccountNumber = receiverAccount.AccountNumber;
        senderTransaction.IBAN = receiverAccount.IBAN;
        senderTransaction.Name = receiverAccount.Name;
        senderTransaction.TransferDescription = request.Model.TransferDescription;
        senderTransaction.Amount = request.Model.Amount;
        senderTransaction.Who = "Sender";
        senderTransaction.PaymentMethod = "EFT";
        senderTransaction.Status = status;
        senderTransaction.TransactionDate = DateTime.UtcNow;
        
        AccountTransaction receiverTransaction = new AccountTransaction();
        receiverTransaction.Id = receiverTransaction.MakeId(receiverTransaction.Id);
        receiverTransaction.AccountId = receiverAccount.Id;
        receiverTransaction.refNumber = refNumber;
        receiverTransaction.AccountNumber = senderAccount.AccountNumber;
        receiverTransaction.IBAN = senderAccount.IBAN;
        receiverTransaction.Name = senderAccount.Name;
        receiverTransaction.TransferDescription = request.Model.TransferDescription;
        receiverTransaction.Amount = request.Model.Amount;
        receiverTransaction.Who = "Receiver";
        receiverTransaction.PaymentMethod = "EFT";
        receiverTransaction.Status = status;
        receiverTransaction.TransactionDate = DateTime.UtcNow;

        unitOfWork.AccountTransactionRepository.AddAsync(senderTransaction, cancellationToken);
        unitOfWork.AccountTransactionRepository.AddAsync(receiverTransaction, cancellationToken);
        unitOfWork.Save();

        var response = mapper.Map<PaymentByEftResponse>(request.Model);
        response.TransactionDate = DateTime.UtcNow;
        response.refNumber = refNumber;
        response.Status = status;
        
        return new ApiResponse<PaymentByEftResponse>(response);
    }
}