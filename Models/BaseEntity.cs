namespace GraphQlApi.Models;

public class BaseEntity
{
    public int Id { get; set; }
    public string? IsActive { get; set; }
    public string? IsDeleted { get; set; }
}
