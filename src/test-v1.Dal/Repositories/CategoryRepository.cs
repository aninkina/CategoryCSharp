using Dapper;
using System.Reflection;
using System.Transactions;
using test_v1.Bll.Models;
using test_v1.Bll.Repositories;
using test_v1.Dal.Entities;
using test_v1.Dal.Extensions;

namespace test_v1.Dal.Repositories;

public class CategoryRepository : ICategoryRepository
{

    private readonly MyDbContext _context;

    public CategoryRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<int> Create(CategoryModel model, CancellationToken cancellationToken = default)
    {
        var connection = _context.CreateConnection();

        var query = """insert into categories ("Name", "ParentId", "Id") values (@name, @parentId, @id) returning "Id";""";

        return await connection.QuerySingleOrDefaultAsync<int>(
            new CommandDefinition(
                query,
                cancellationToken: cancellationToken,
                parameters: new { model.Name, model.ParentId, model.Id }));
    }

    public async Task<CategoryModel> GetById(int id, CancellationToken cancellationToken = default)
    {
        var connection = _context.CreateConnection();

        var query = """select * from categories where "Id" = @id;""";

        var result = await connection.QuerySingleOrDefaultAsync<Category>(
            new CommandDefinition(
                query,
                cancellationToken: cancellationToken,
                parameters: new { id }));

        return result.ToCategoryModel();
    }

    public async Task<CategoryDetailedModel> GetChildrenById(int id, CancellationToken cancellationToken = default)
    {
        var connection = _context.CreateConnection();

        var query =
            @"WITH RECURSIVE rec AS (
                SELECT @id as id, c1.""ParentId"", c1.""Name"" from categories as c1 where c1.""Id"" = @id
                UNION ALL
                SELECT c2.""Id"", c2.""ParentId"", c2.""Name""
                FROM categories AS c2
                    JOIN rec ON rec.id = c2.""ParentId""
            )   
            SELECT * FROM rec;";

        var result = await connection.QueryAsync<Category>(
            new CommandDefinition(
                query,
                cancellationToken: cancellationToken,
                parameters: new { id }));

        var rootCategory = result.First().ToCategoryModel();

        var categoryList = result
            .Select(x => x.ToCategoryModel())
            .Skip(1)
            .ToList();

        return new CategoryDetailedModel { Root = rootCategory, CategoryList = categoryList };
    }

    public async Task<int> Update(CategoryModel model, CancellationToken cancellationToken = default)
    {
        var connection = _context.CreateConnection();

        var query = """update categories set "Name" = @name, "ParentId" = @parentId where "Id" = @id returning "Id"; """;

        return await connection.QuerySingleOrDefaultAsync<int>(
            new CommandDefinition(
                query,
                cancellationToken: cancellationToken,
                parameters: new { model.Name, model.ParentId, model.Id }));
    }
}
