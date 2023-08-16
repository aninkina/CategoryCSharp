using test_v1.Bll.Models.Requests;
using test_v1.Dal.Entities;

namespace test_v1.Bll.Repositories;

public interface ICategoryRepository
{
    Task<int> Create(CreateCategoryRequest request, CancellationToken cancellationToken = default);

    Task<int> Update(UpdateCategoryRequest request, CancellationToken cancellationToken = default);

    Task<Category> GetById(int id, CancellationToken cancellationToken = default);

    Task<ICollection<Category>> GetChildrenById(int id, CancellationToken cancellationToken = default);
}
