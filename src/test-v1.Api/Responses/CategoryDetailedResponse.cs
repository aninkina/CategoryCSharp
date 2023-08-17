namespace test_v1.Api.Responses;

public sealed class CategoryDetailedResponse
{
    public CategoryResponse Root { get; set; } = default!;
    public ICollection<CategoryResponse> CategoryList { get; set; } = new List<CategoryResponse>();
}
