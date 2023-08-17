namespace test_v1.Bll.Models;
public sealed class CategoryDetailedModel
{
    public  CategoryModel Root { get; set; } = default!;
    public ICollection<CategoryModel> CategoryList { get; set; } = new List<CategoryModel>();
}
