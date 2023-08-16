using FluentValidation;
using test_v1.Bll.Models.Requests;
using test_v1.Bll.Repositories;
using test_v1.Bll.Validators;

namespace test_v1.Bll.Services;

public sealed class CategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<BaseApiResponse> Create(CreateCategoryRequest model, CancellationToken cancellationToken = default)
    {
        var validator = new CreateCategoryValidator();
        validator.ValidateAndThrow(model);
        
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

    public async Task<BaseApiResponse> Update( UpdateCategoryRequest model, CancellationToken cancellationToken = default)
    {
        var validator = new UpdateCategoryValidator();
        validator.ValidateAndThrow(model);

        var parentExists = await _repository.GetById(model.ParentId, cancellationToken);

        if (parentExists == null)
        {
            return BaseApiResponse.Error("Parent doesn't exist");
        }

        var response = await _repository.Update(model, cancellationToken);

        return BaseApiResponse.Success();
    }
}
