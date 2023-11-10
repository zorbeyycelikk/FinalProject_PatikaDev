using AutoMapper;
using MediatR;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;

namespace Vk.Operation.Command;

public class NoticeCommandHandler:
    IRequestHandler<CreateNoticeCommand, ApiResponse>,
    IRequestHandler<UpdateNoticeCommand, ApiResponse>,
    IRequestHandler<DeleteNoticeCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public NoticeCommandHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(CreateNoticeCommand request, CancellationToken cancellationToken)
    {
        Notice mapped = mapper.Map<Notice>(request.Model);
        mapped.Id = mapped.MakeId(mapped.Id);

        unitOfWork.NoticeRepository.AddAsync(mapped,cancellationToken);
        unitOfWork.Save();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateNoticeCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.NoticeRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }

        entity.Content = request.Model.Content;
        entity.ReadStatus = request.Model.ReadStatus;
        
        unitOfWork.NoticeRepository.Update(entity);
        unitOfWork.Save();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteNoticeCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.NoticeRepository.GetById(request.Id, cancellationToken);
        if (entity is null)
        {
            return new ApiResponse("Error");
        }
        unitOfWork.NoticeRepository.Remove(request.Id);
        unitOfWork.Save();
        
        return new ApiResponse();
    }
}