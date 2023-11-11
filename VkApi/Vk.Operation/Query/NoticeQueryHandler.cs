using LinqKit;
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
    IRequestHandler<GetNoticeById, ApiResponse<NoticeResponse>>,
    IRequestHandler<GetNoticeByReceiverId, ApiResponse<List<NoticeResponse>>>,
    IRequestHandler<GetNoticeByParametersQuery, ApiResponse<List<NoticeResponse>>>
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

    public async Task<ApiResponse<List<NoticeResponse>>> Handle(GetNoticeByReceiverId request, CancellationToken cancellationToken)
    {
        List<Notice> notices = await unitOfWork.NoticeRepository.GetAsQueryable()
            .Where(x => x.ReceiverId == request.Id).ToListAsync(cancellationToken);
        if (!notices.Any())
        {
            return new ApiResponse<List<NoticeResponse>>("Error" , false);
        }
        List<NoticeResponse> response = mapper.Map<List<NoticeResponse>>(notices);
        return new ApiResponse<List<NoticeResponse>>(response);
    }
    
    public async Task<ApiResponse<List<NoticeResponse>>> Handle(GetNoticeByParametersQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Notice>(true);
        if (!string.IsNullOrWhiteSpace(request.Id))
            predicate.And(x => x.Id == request.Id);
        if (!string.IsNullOrWhiteSpace(request.ReceiverId))
            predicate.And(x => x.ReceiverId == request.ReceiverId);
        if (!string.IsNullOrWhiteSpace(request.Content))
            predicate.And(x => x.Content.Contains(request.Content));
        if (request.ReadStatus != null)
            predicate.And(x => x.ReadStatus == true || x.ReadStatus == false);
        
        List<Notice> notices = await unitOfWork.NoticeRepository.GetAsQueryable()
            .Where(predicate).ToListAsync(cancellationToken);

        if (!notices.Any())
        {
            return new ApiResponse<List<NoticeResponse>>("Error");
        }
        
        var mapped = mapper.Map<List<NoticeResponse>>(notices);
        return new ApiResponse<List<NoticeResponse>>(mapped);
    }
    
}