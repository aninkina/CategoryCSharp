using test_v1.Bll.Models;
using test_v1.Bll.Repositories;

namespace test_v1.Bll.Services;

public sealed class CategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<BaseApiResponse> Create(CategoryModel model, CancellationToken cancellationToken = default)
    {
        var categoryExists = await _repository.GetById(model.Id, cancellationToken);

        if (categoryExists != null)
        {
            return BaseApiResponse.Error("Category already exists");
        }

        var parentExists = await _repository.GetById(model.ParentId, cancellationToken);

        if (parentExists == null)
        {
            return BaseApiResponse.Error("Parent doesn't exist");
        }

        var createdId = await _repository.Create(model, cancellationToken);


        return BaseApiResponse.Success($"Id = {createdId}");
    }

    public async Task<BaseApiResponse> Update(CategoryModel model, CancellationToken cancellationToken = default)
    {
        var parentExists = await _repository.GetById(model.ParentId, cancellationToken);

        if (parentExists == null)
        {
            return BaseApiResponse.Error("Parent doesn't exist");
        }

        var updatedId = await _repository.Update(model, cancellationToken);

        return BaseApiResponse.Success($"Id = {updatedId}");
    }
}
