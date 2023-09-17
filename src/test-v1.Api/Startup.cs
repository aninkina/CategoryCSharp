using FluentValidation.AspNetCore;
using test_v1.Dal;
using test_v1.Api.Middlewaries;
using test_v1.Bll.Repositories;
using test_v1.Bll.Services;
using test_v1.Dal.Extensions;
using test_v1.Dal.Repositories;
using test_v1.Dal.Settings;
using FluentValidation;
using System;
using test_v1.Api.Requests;
using test_v1.Api.Validators;

namespace test_v1.Api;

public sealed class Startup
{
    IConfiguration Configuration { get; }
    IWebHostEnvironment Environment { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.Configure<DbConnectionOptions>(Configuration.GetSection(nameof(DbConnectionOptions)));

        services.AddSingleton<MyDbContext>();

        services.AddScoped<CategoryService>();
        services.AddScoped<CategoryQueryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IValidator<CreateCategoryRequest>, CreateCategoryValidator>();
        services.AddScoped<IValidator<UpdateCategoryRequest>, UpdateCategoryValidator>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddFluentValidation(conf =>
        {
            conf.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
            conf.AutomaticValidationEnabled = true;
        });

        services.AddMigrations();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.MigrateUp();


        app.UseSwagger();
        app.UseSwaggerUI();


        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseMiddleware<ErrorMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
