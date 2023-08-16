namespace test_v1.Bll.Models.Responses;

public sealed class CategoryDetailedModel
{
    public CategoryModel Category { get; set; } = default!;
    public ICollection<CategoryModel> Subcategories { get; set; } = new List<CategoryModel>();
}
