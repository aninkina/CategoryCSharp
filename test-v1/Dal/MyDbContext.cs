using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace test_v1.Dal;

public sealed class MyDbContext
{
    private readonly DbConnectionOptions _dbOptions;

    public MyDbContext(IOptions<DbConnectionOptions> dbOptions)
    {
        _dbOptions = dbOptions.Value;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_dbOptions.ConnectionString);
    }
}