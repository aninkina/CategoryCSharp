using test_v1.Api.Requests;
using test_v1.Api.Responses;
using test_v1.Bll.Models;

namespace test_v1.Api.Extensions;

public static class CategoryMapperExtensions
{
    public static CategoryResponse ToCategoryResponse(this CategoryModel model)
    {
        return new CategoryResponse { Id = model.Id, Name = model.Name, ParentId = model.ParentId };
    }

    public static CategoryModel ToCategoryModel(this CreateCategoryRequest request)
    {
        return new CategoryModel { Id = request.Id, Name = request.Name, ParentId = request.ParentId };
    }

    public static CategoryModel ToCategoryModel(this UpdateCategoryRequest request)
    {
        return new CategoryModel { Id = request.Id, Name = request.Name, ParentId = request.ParentId };
    }

    public static CategoryDetailedResponse ToCategoryDetailedResponse(this CategoryDetailedModel model)
    {
        return new CategoryDetailedResponse
        {
            Root = model.Root.ToCategoryResponse(),
            CategoryList = model.CategoryList.Select(x => x.ToCategoryResponse()).ToList()
        };
    }
}
