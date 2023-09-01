using test_v1.Bll.Models;

namespace test_v1.Bll.Repositories;

public interface ICategoryRepository
{
    Task<int> Create(CategoryModel model, CancellationToken cancellationToken = default);

    Task<int> Update(CategoryModel model, CancellationToken cancellationToken = default);

    Task<CategoryModel?> GetById(int id, CancellationToken cancellationToken = default);

    Task<CategoryDetailedModel> GetChildrenById(int id, CancellationToken cancellationToken = default);
}
