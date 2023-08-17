using test_v1.Bll.Models;
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
        var categoryExists = await _repository.GetById(id, cancellationToken);

        if (categoryExists == null)
        {
            return null;
        }

        return await _repository.GetChildrenById(id, cancellationToken);
    }
}
