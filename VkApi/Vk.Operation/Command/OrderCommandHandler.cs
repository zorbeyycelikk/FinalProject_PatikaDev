using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class OrderCommandHandler:
    IRequestHandler<CreateOrderCommand, ApiResponse>,
    IRequestHandler<UpdateOrderCommand, ApiResponse>,
    IRequestHandler<DeleteOrderCommand, ApiResponse>,
    IRequestHandler<ConfirmWithOrderNumberCommand , ApiResponse>,
    IRequestHandler<ConfirmWithIdCommand , ApiResponse>,
    IRequestHandler<CancelledWithOrderNumberCommand , ApiResponse>


{
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public OrderCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IMediator mediator
        )
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.mediator = mediator;
    }

    public async Task<ApiResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order mapped = mapper.Map<Order>(request.Model);
        var refNumber = Guid.NewGuid().ToString().Replace("-", "").ToLower();
        mapped.Id = mapped.MakeId(mapped.Id);
        mapped.Status = "Failed";
        mapped.OrderNumber = refNumber;
        mapped.InsertDate = DateTime.UtcNow;

        if (request.Model.PaymentRefCode == " ")
        {
            unitOfWork.OrderRepository.AddAsync(mapped,cancellationToken);
            unitOfWork.Save();
            return new ApiResponse("Error");
        }
        
        var checkStock = await new checkStockCommand(unitOfWork).CheckStock(request.Model.BasketId, cancellationToken);
        if (!checkStock.Success)
        {
            unitOfWork.OrderRepository.AddAsync(mapped,cancellationToken);
            unitOfWork.Save();
            return new ApiResponse("Error");
        }
        
        mapped.Status = "Successful";
        unitOfWork.OrderRepository.AddAsync(mapped,cancellationToken);
        unitOfWork.Save();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.OrderRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }

        entity.Address = request.Model.Address;
        entity.Description = request.Model.Description;
        entity.Status = request.Model.Status;
        
        unitOfWork.OrderRepository.Update(entity);
        unitOfWork.Save();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.OrderRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        unitOfWork.OrderRepository.Remove(request.Id);
        unitOfWork.Save();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(ConfirmWithOrderNumberCommand request, CancellationToken cancellationToken)
    {
        var x = await unitOfWork.OrderRepository.GetAsQueryable()
            .Where(x => x.OrderNumber == request.orderNumber && x.Status == "Successful")
            .SingleOrDefaultAsync(cancellationToken);
        
        if (x is null)
        {
            return new ApiResponse("Error");
        }
        
        x.Status = "Confirm";
        unitOfWork.OrderRepository.Update(x);
        unitOfWork.Save();
        return new ApiResponse();
    }
    
    public async Task<ApiResponse> Handle(ConfirmWithIdCommand request, CancellationToken cancellationToken)
    {
        var x = await unitOfWork.OrderRepository.GetAsQueryable()
            .Where(x => x.Id == request.id && x.Status == "Successful")
            .SingleOrDefaultAsync(cancellationToken);
        
        if (x is null)
        {
            return new ApiResponse("Error");
        }
        
        x.Status = "Confirm";
        unitOfWork.OrderRepository.Update(x);
        unitOfWork.Save();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(CancelledWithOrderNumberCommand request, CancellationToken cancellationToken)
    {
        var x = await unitOfWork.OrderRepository.GetAsQueryable()
            .Where(x => x.OrderNumber == request.orderNumber && x.Status == "Successful")
            .SingleOrDefaultAsync(cancellationToken);
       
        if (x is null)
        {
            return new ApiResponse("Error");
        }

        x.Status = "Cancelled";
        
        if (x.PaymentMethod == "EFT" || x.PaymentMethod == "Havale")
        {
           var paymentEft = await cancelledForEftOrHavale(x.PaymentRefCode, cancellationToken);
           if (!paymentEft.Success)
           {
               return new ApiResponse("Error");
           }
        }

        if (x.PaymentMethod == "Card")
        {
            var paymentCard = await cancelledForCard(x.PaymentRefCode, cancellationToken);
            if (!paymentCard.Success)
            {
                return new ApiResponse("Error");
            }
        }
        
        if (x.PaymentMethod == "Open Account")
        {
            var paymentOpenAccount = await cancelledForCard(x.PaymentRefCode, cancellationToken);
            if (!paymentOpenAccount.Success)
            {
                return new ApiResponse("Error");
            }
        }
        
        
        await mediator.Send(new UpdateProductStockAfterCancelledOrderCommand(x.BasketId));
        return new ApiResponse();
    }
    
    private async Task<ApiResponse> cancelledForEftOrHavale(string refNumber , CancellationToken cancellationToken)
    {
        var senderAccount = await unitOfWork.AccountTransactionRepository.GetAsQueryable("Account")
            .Where(x => x.refNumber == refNumber && x.Who == "Sender").SingleOrDefaultAsync(cancellationToken);
        
        var receiverAccount =await unitOfWork.AccountTransactionRepository.GetAsQueryable("Account")
            .Where(x => x.refNumber == refNumber && x.Who == "Receiver").SingleOrDefaultAsync(cancellationToken);

        // Ödeme İslemi
        CreatePaymentByHavaleRequest havReq = new CreatePaymentByHavaleRequest
        {
            SenderAccountNumber = senderAccount.AccountNumber,
            AccountNumber = receiverAccount.AccountNumber,
            Name = receiverAccount.Name,
            TransferDescription = "Return of " + refNumber,
            Amount = senderAccount.Amount
        };
        var resultPayment = await mediator.Send(new CreatePaymentByHavaleTransferCommand(havReq));
        if (!resultPayment.Success)
        {
            return new ApiResponse("Error");
        }
        return new ApiResponse();
    }
    
    private async Task<ApiResponse> cancelledForCard(string refNumber , CancellationToken cancellationToken)
    {
        var receiverCardTransaction = await unitOfWork.CardTransactionRepository
            .GetAsQueryable("Card")
            .Where(x => x.transactionRefNumber == refNumber && x.Status == "Successful")
            .SingleOrDefaultAsync(cancellationToken);
        
        var receiverCardInfo = await unitOfWork.CardRepository.GetAsQueryable("Account")
            .Where(x => x.Id== receiverCardTransaction.CardId)
            .SingleOrDefaultAsync(cancellationToken);

        var receiverAccount = await unitOfWork.AccountRepository.GetById(receiverCardInfo.AccountId, cancellationToken, "Customer","Card");
        
        var senderAccount =await unitOfWork.AccountTransactionRepository.GetAsQueryable("Account")
            .Where(x => x.refNumber == refNumber && x.Who == "Receiver").SingleOrDefaultAsync(cancellationToken);

        // Ödeme İslemi
        CreatePaymentByHavaleRequest havReq = new CreatePaymentByHavaleRequest
        {
            SenderAccountNumber = senderAccount.AccountNumber,
            AccountNumber = receiverAccount.AccountNumber,
            Name = receiverAccount.Name,
            TransferDescription = "Return of " + refNumber,
            Amount = senderAccount.Amount
        };
        var resultPayment = await mediator.Send(new CreatePaymentByHavaleTransferCommand(havReq));
        if (!resultPayment.Success)
        {
            return new ApiResponse("Error");
        }
        return new ApiResponse();
    }
    
    private async Task<ApiResponse> cancelledForOpenAccount(string refNumber , CancellationToken cancellationToken)
    {
        var senderAccount = await unitOfWork.OpenAccountTransactionRepository.GetAsQueryable("Customer")
            .Where(x => x.refNumber == refNumber && x.Who == "Sender").SingleOrDefaultAsync(cancellationToken);

        var user = await unitOfWork.CustomerRepository.GetById(senderAccount.CustomerId, cancellationToken);

        if (user is null)
        {
            return new ApiResponse("Error");

        }
        user.openAccountLimit += senderAccount.Amount;
        return new ApiResponse();
    }
}