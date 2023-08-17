using test_v1.Bll.Models;
using test_v1.Dal.Entities;

namespace test_v1.Dal.Extensions;

public static class CategoryMapperExtensions
{
    public static CategoryModel ToCategoryModel(this Category category)
    {
        return new CategoryModel { Id = category.Id, Name = category.Name, ParentId = category.ParentId }; 
    }
}
