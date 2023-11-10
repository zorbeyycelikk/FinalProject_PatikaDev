using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        var receiverAccount = await unitOfWork.AccountRepository.GetAsQueryable()
            .Where(x => x.AccountNumber == request.Model.receiverAccountNumber && x.IsActive == true)
            .SingleOrDefaultAsync(cancellationToken);

        if (receiverAccount is null)
        {
            return new ApiResponse<PaymentByCardResponse>("Error", false);
        }
        
        // girilen bilgilerle eşleşen bir card mevcut mu ?

        var card = await unitOfWork.CardRepository.GetAsQueryable()
            .Where(x => x.IsActive == true &&
                        x.Cvv == request.Model.Cvv &&
                        x.ExpiryDate == request.Model.ExpiryDate &&
                        x.CardNumber == request.Model.CardNumber).SingleOrDefaultAsync(cancellationToken);
        if (card is null)
        {
            return new ApiResponse<PaymentByCardResponse>("Error", false);
        }
        
        // Card'ın baglı hesabın balance'ına bakılır.
        var senderAccount = await unitOfWork.AccountRepository.GetAsQueryable()
            .Where(x => x.Id == card.AccountId).SingleOrDefaultAsync(cancellationToken);

        // Ödemek için yeterli miktarda para yoktur.
        if (senderAccount.Balance < request.Model.Amount)
        {
            return new ApiResponse<PaymentByCardResponse>("Yetersiz Bakiye", false);
        }
        
    receiverAccount.Balance = receiverAccount.Balance + request.Model.Amount;
    senderAccount.Balance = senderAccount.Balance - request.Model.Amount;
    
    var refNumber = Guid.NewGuid().ToString().Replace("-", "").ToLower();
    var status = "Successful";
    
    CardTransaction senderTransaction = new CardTransaction();
    senderTransaction.Id = senderTransaction.MakeId(senderTransaction.Id);
    senderTransaction.CardId = card.Id;
    senderTransaction.transactionRefNumber = refNumber;
    senderTransaction.receiverAccountNumber = request.Model.receiverAccountNumber;
    senderTransaction.CardNumber = request.Model.CardNumber;
    senderTransaction.Cvv = request.Model.Cvv;
    senderTransaction.ExpiryDate = request.Model.ExpiryDate;
    senderTransaction.Amount = request.Model.Amount;
    senderTransaction.Status = status;
    
    AccountTransaction receiverTransaction = new AccountTransaction();
    receiverTransaction.Id = receiverTransaction.MakeId(receiverTransaction.Id);
    receiverTransaction.AccountId = receiverAccount.Id;
    receiverTransaction.refNumber = refNumber;
    receiverTransaction.AccountNumber = senderAccount.AccountNumber;
    receiverTransaction.IBAN = senderAccount.IBAN;
    receiverTransaction.Name = senderAccount.Name;
    receiverTransaction.TransferDescription = "Card Payment";
    receiverTransaction.Amount = request.Model.Amount;
    receiverTransaction.Who = "Receiver";
    receiverTransaction.PaymentMethod = "Card";
    receiverTransaction.Status = status;
    receiverTransaction.TransactionDate = DateTime.UtcNow;
    
    unitOfWork.CardTransactionRepository.AddAsync(senderTransaction, cancellationToken);
    unitOfWork.AccountTransactionRepository.AddAsync(receiverTransaction, cancellationToken);
    unitOfWork.Save();


    var response = mapper.Map<PaymentByCardResponse>(request.Model);
    response.transactionRefNumber = refNumber;
    response.Status = status;
    response.CardId = card.Id;
    
    return new ApiResponse<PaymentByCardResponse>(response); 
    }
    
}