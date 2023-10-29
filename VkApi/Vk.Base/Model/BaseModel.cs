namespace Vk.Base.Model;

public class BaseModel
{
    public Guid Id { get; set; }
    public DateTime InsertDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsActive { get; set; }
}