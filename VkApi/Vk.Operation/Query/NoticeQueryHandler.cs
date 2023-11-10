using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

using AutoMapper;
using MediatR;

public class NoticeQueryHandler :
    IRequestHandler<GetAllNoticeQuery, ApiResponse<List<NoticeResponse>>>,
    IRequestHandler<GetNoticeById, ApiResponse<NoticeResponse>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    
    public NoticeQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<List<NoticeResponse>>> Handle(GetAllNoticeQuery request, CancellationToken cancellationToken)
    {
        List<Notice> x =  await unitOfWork.NoticeRepository.GetAll(cancellationToken,"Account");
        List<NoticeResponse> response = mapper.Map<List<NoticeResponse>>(x);
        return new ApiResponse<List<NoticeResponse>>(response);
    }

    public async Task<ApiResponse<NoticeResponse>> Handle(GetNoticeById request, CancellationToken cancellationToken)
    {
        Notice x = await unitOfWork.NoticeRepository.GetById(request.Id, cancellationToken,"Account");
        
        if (x is null)
        {
            return new ApiResponse<NoticeResponse>("Error" , false);
        }
        
        NoticeResponse response = mapper.Map<NoticeResponse>(x);
        return new ApiResponse<NoticeResponse>(response);
    }
    
}