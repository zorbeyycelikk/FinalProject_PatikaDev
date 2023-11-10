namespace Vk.Schema;

public class CreateNoticeRequest
{ 
    public string ReceiverId { get; set; }
    public string Content { get; set; }
    public bool ReadStatus { get; set; }
}

public class UpdateNoticeRequest
{
    public string Content { get; set; }
    public bool ReadStatus { get; set; }
}

public class NoticeResponse
{
    public string ReceiverId { get; set; }
    public string Content { get; set; }
    public bool ReadStatus { get; set; }
}

