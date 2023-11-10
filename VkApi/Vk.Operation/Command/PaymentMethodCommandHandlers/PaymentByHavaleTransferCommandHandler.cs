using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class PaymentByHavaleTransferCommandHandler:
    IRequestHandler<CreatePaymentByHavaleTransferCommand, ApiResponse<PaymentByHavaleResponse>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public PaymentByHavaleTransferCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<PaymentByHavaleResponse>> Handle(CreatePaymentByHavaleTransferCommand request, CancellationToken cancellationToken)
    {
        // böyle bir account numarasına sahip aktif bir hesap ve alıcı ismi ile eşleşen hesap var mı ?
        var checkreceiverAccount  = await CheckAccount(request.Model.AccountNumber , request.Model.Name, cancellationToken);
        
        if (!checkreceiverAccount.Success)
        {
            return new ApiResponse<PaymentByHavaleResponse>(checkreceiverAccount.Message);
        }

        var responseCheckBalance = await CheckBalance(request.Model.SenderAccountNumber, request.Model.Amount, cancellationToken);
        // Ödemek için yeterli miktarda para yoktur.
        if (!responseCheckBalance.Success)
        {
            return new ApiResponse<PaymentByHavaleResponse>("Error", false);
        }
        
        var receiverAccount = checkreceiverAccount.Response;
        var senderAccount = responseCheckBalance.Response;
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
        senderTransaction.PaymentMethod = "Havale";
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
        receiverTransaction.PaymentMethod = "Havale";
        receiverTransaction.Status = status;
        receiverTransaction.TransactionDate = DateTime.UtcNow;

        unitOfWork.AccountTransactionRepository.AddAsync(senderTransaction, cancellationToken);
        unitOfWork.AccountTransactionRepository.AddAsync(receiverTransaction, cancellationToken);
        unitOfWork.Save();

        var response = mapper.Map<PaymentByHavaleResponse>(request.Model);
        response.TransactionDate = DateTime.UtcNow;
        response.refNumber = refNumber;
        response.Status = status;
        
        return new ApiResponse<PaymentByHavaleResponse>(response); // değişecek
    }

    public async Task<ApiResponse<Account>> CheckBalance(string accountNumber,decimal amount , CancellationToken cancellationToken)
    {
        var account = await unitOfWork.AccountRepository.GetAsQueryable()
            .Where(x => x.AccountNumber == accountNumber).SingleOrDefaultAsync(cancellationToken);

        // Ödemek için yeterli miktarda para yoktur.
        if (account.Balance < amount)
        {
            return new ApiResponse<Account>("Error", false);
        }
        return new ApiResponse<Account>(account);
    }

    private async Task<ApiResponse<Account>> CheckAccount(string accountNumber, string name ,CancellationToken cancellationToken)
    {
        var receiverAccount  = await unitOfWork.AccountRepository.GetAsQueryable()
            .Where(x => x.AccountNumber == accountNumber && x.IsActive == true && x.Name == name)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (receiverAccount == null)
        {
            return new ApiResponse<Account>("Invalid Account",false);
        }

        if (receiverAccount.IsActive == false)
        {
            return new ApiResponse<Account>("Invalid Account",false);
        }
        return new ApiResponse<Account>(receiverAccount);
    }
}