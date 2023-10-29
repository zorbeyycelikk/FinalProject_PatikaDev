namespace Vk.Base;

public abstract class BaseModel
{
    public int Id { get; set; }
    public DateTime InsertDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsActive { get; set; }
}