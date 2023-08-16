using Microsoft.AspNetCore.Mvc;
using test_v1.Bll.Models.Requests;
using test_v1.Bll.Models.Responses;
using test_v1.Bll.Services;

namespace test_v1.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CategoryController
{
    private readonly ILogger<CategoryController> _logger;

    private readonly CategoryService _service;

    private readonly CategoryQueryService _queryService;


    public CategoryController(ILogger<CategoryController> logger, CategoryService service, CategoryQueryService queryService)
    {
        _logger = logger;
        _service = service;
        _queryService = queryService;
    }

    [HttpPost("Create")]
    public Task<BaseApiResponse> Create([FromBody] CreateCategoryRequest model, CancellationToken cancellationToken = default)
    {
        return _service.Create(model, cancellationToken);
    }

    [HttpPost("Update")]
    public Task<BaseApiResponse> Update([FromBody] UpdateCategoryRequest model, CancellationToken cancellationToken = default)
    {
        return _service.Update(model, cancellationToken);
    }

    [HttpGet("GetById/{id}")]
    public Task<CategoryModel?> GetById([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        return _queryService.GetById(id, cancellationToken);
    }

    [HttpGet("GetChildrenById/{id}")]
    public Task<CategoryDetailedModel?> GetChildrenById([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        return _queryService.GetChildrenById(id, cancellationToken);
    }
}
