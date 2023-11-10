using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public class NoticeCqrs
{
    public record CreateNoticeCommand(CreateNoticeRequest Model) : IRequest<ApiResponse>;
    public record UpdateNoticeCommand(UpdateNoticeRequest Model,string Id) : IRequest<ApiResponse>;
    public record DeleteNoticeCommand(string Id) : IRequest<ApiResponse>;

    public record GetAllNoticeQuery() : IRequest<ApiResponse<List<NoticeResponse>>>;
    public record GetNoticeById(string Id) : IRequest<ApiResponse<NoticeResponse>>;
}