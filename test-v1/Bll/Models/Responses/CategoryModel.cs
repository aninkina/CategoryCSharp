namespace test_v1.Bll.Models.Responses;

public sealed class CategoryModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ParentId { get; set; } 
}
