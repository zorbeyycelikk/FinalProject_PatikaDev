using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;

namespace Vk.Operation.Command;

public class BasketCommandHandler:
    IRequestHandler<CreateBasketCommand, ApiResponse>,
    IRequestHandler<DeleteBasketCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public BasketCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        var x = await unitOfWork.BasketRepository.GetAsQueryable()
            .Where(x => x.CustomerId == request.Model.CustomerId && x.IsActive == true).SingleOrDefaultAsync(cancellationToken);
        //Eğer aktif bir hesabı var ise yeni hesap yaratmaz
        if (x is not null)
        {
            return new ApiResponse("Error");
        }
        Basket mapped = mapper.Map<Basket>(request.Model);
        mapped.Id = mapped.MakeId(mapped.Id);

        unitOfWork.BasketRepository.AddAsync(mapped,cancellationToken);
        unitOfWork.Save();
        
        return new ApiResponse();
    }


    public async Task<ApiResponse> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.BasketRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        unitOfWork.BasketRepository.Remove(request.Id);
        unitOfWork.Save();
        
        return new ApiResponse();
    }
}