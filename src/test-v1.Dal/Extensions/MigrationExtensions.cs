using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using test_v1.Dal.Settings;

namespace test_v1.Dal.Extensions;

public static class MigrationExtensions
{
    public static IApplicationBuilder MigrateUp(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();

        return app;
    }

    public static IServiceCollection AddMigrations(this IServiceCollection services)
    {
        services.AddFluentMigratorCore()
                      .ConfigureRunner(cfg => cfg
                          .AddPostgres()
                          .WithGlobalConnectionString(s =>
                          {
                              var cfg = s.GetRequiredService<IOptions<DbConnectionOptions>>();
                              return cfg.Value.ConnectionString;
                          })
                          .ScanIn(typeof(MigrationExtensions).Assembly).For.Migrations()
                      )
                      .AddLogging(cfg => cfg.AddFluentMigratorConsole());

        return services;
    }
}
