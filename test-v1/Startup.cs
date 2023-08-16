using FluentMigrator.Runner;
using Microsoft.AspNetCore.Hosting.Server;
using test_v1.Bll.Repositories;
using test_v1.Bll.Services;
using test_v1.Dal;
using test_v1.Dal.Repositories;
using test_v1.Migrations;

namespace test_v1;

public sealed class Startup
{
    IConfiguration Configuration { get; }
    IWebHostEnvironment Environment { get; }

    private readonly DbConnectionOptions? _dbOptions;

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
        _dbOptions = Configuration.GetSection(nameof(DbConnectionOptions)).Get<DbConnectionOptions>();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllers();

        // configure strongly typed settings object
        services.Configure<DbConnectionOptions>(Configuration.GetSection("DbConnectionOptions"));

        // configure DI for application services
        services.AddSingleton<MyDbContext>();
        services.AddScoped<CategoryService>();
        services.AddScoped<CategoryQueryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddFluentMigratorCore()
                      .ConfigureRunner(cfg => cfg
                          .AddPostgres()
                          .WithGlobalConnectionString(_dbOptions.ConnectionString)
                          .ScanIn(typeof(Startup).Assembly).For.Migrations()
                      )
                      .AddLogging(cfg => cfg.AddFluentMigratorConsole());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
    {
        migrationRunner.MigrateUp();

        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseMiddleware<ErrorMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }
}
