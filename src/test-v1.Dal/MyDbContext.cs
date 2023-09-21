using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using test_v1.Dal.Settings;

namespace test_v1.Dal;

public sealed class MyDbContext
{
    private readonly ILogger<MyDbContext> _logger;
    private readonly DbConnectionOptions _dbOptions;

    public MyDbContext(IOptions<DbConnectionOptions> dbOptions, ILogger<MyDbContext> logger)
    {
        _dbOptions = dbOptions.Value;
        _logger = logger;
    }

    public IDbConnection CreateConnection()
    {
        _logger.LogInformation("DbContext conString: {con}", _dbOptions.ConnectionString);
        return new NpgsqlConnection(_dbOptions.ConnectionString);
    }
}