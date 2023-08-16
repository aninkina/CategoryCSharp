namespace test_v1.Bll.Models.Requests;

public sealed class UpdateCategoryRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ParentId { get; set; }
}
