namespace Vk.Base;

public abstract class BaseModel
{
    public string Id { get; set; }
    public DateTime InsertDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsActive { get; set; }

    public string MakeId(string id)
    {
        id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
        return id;
    }
}