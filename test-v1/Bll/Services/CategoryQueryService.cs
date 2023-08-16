using test_v1.Bll.Models.Responses;
using test_v1.Bll.Repositories;

namespace test_v1.Bll.Services;

public sealed class CategoryQueryService
{
    private readonly ICategoryRepository _repository;

    public CategoryQueryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<CategoryModel?> GetById(int id, CancellationToken cancellationToken = default)
    {
        if(id < 0)
        {
            return null;
        }

        var category = await _repository.GetById(id, cancellationToken);

        if (category == null)
        {
            return null;
        }

        return new CategoryModel
        {
            Id = category.Id,
            Name = category.Name,
            ParentId = category.ParentId
        };
    }

    public async Task<CategoryDetailedModel?> GetChildrenById(int id, CancellationToken cancellationToken)
    {
        if (id < 0)
        {
            return null;
        }

        var categoryExists = await _repository.GetById(id, cancellationToken);

        if (categoryExists == null)
        {
            return null;
        }

        var categoryList = await _repository.GetChildrenById(id, cancellationToken);

        if (categoryList == null)
        {
            return null;
        }

        var parent = categoryList.First();

        return new CategoryDetailedModel
        {
            Category = new CategoryModel { Id = parent.Id, Name = parent.Name, ParentId = parent.ParentId },
            Subcategories = categoryList.
                Select(x => new CategoryModel { Id = x.Id, Name = x.Name, ParentId = x.ParentId }).
                Skip(1).ToList()
        };
    }
}
