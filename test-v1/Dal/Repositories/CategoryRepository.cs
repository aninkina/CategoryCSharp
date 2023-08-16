using Dapper;
using test_v1.Bll.Models.Requests;
using test_v1.Bll.Repositories;
using test_v1.Dal.Entities;

namespace test_v1.Dal.Repositories;

public class CategoryRepository : ICategoryRepository
{

    private readonly MyDbContext _context;

    public CategoryRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<int> Create(CreateCategoryRequest model, CancellationToken cancellationToken = default)
    {
        var connection = _context.CreateConnection();

        var query = """insert into categories ("Name", "ParentId", "Id") values (@name, @parentId, @id) returning "Id";""";

        return await connection.QuerySingleAsync<int>(query, new { model.Name, model.ParentId, model.Id });
    }


    public async Task<Category> GetById(int id, CancellationToken cancellationToken = default)
    {
        var connection = _context.CreateConnection();

        var query = """select * from categories where "Id" = @id;""";

        return await connection.QuerySingleOrDefaultAsync<Category>(query, new { id });
    }

    public async Task<ICollection<Category>> GetChildrenById(int id, CancellationToken cancellationToken = default)
    {
        var connection = _context.CreateConnection();

        var query =
            @"WITH RECURSIVE rec AS (
                SELECT 1 as id, c1.""ParentId"", c1.""Name"" from categories as c1 where c1.""Id"" = 1
                UNION ALL
                SELECT c2.""Id"", c2.""ParentId"", c2.""Name""
                FROM categories AS c2
                    JOIN rec ON rec.id = c2.""ParentId""
            )   
            SELECT * FROM rec;";

        return (await connection.QueryAsync<Category>(query, new { id })).AsList();
    }

    public async Task<int> Update(UpdateCategoryRequest model, CancellationToken cancellationToken = default)
    {
        var connection = _context.CreateConnection();

        var query = """update categories set "Name" = @name, "ParentId" = @parentId where "Id" = @id returning "Id"; """;

        return await connection.QuerySingleAsync<int>(new CommandDefinition(
            query, cancellationToken: cancellationToken, parameters: new { model.Name, model.ParentId, model.Id })
            );
    }
}
