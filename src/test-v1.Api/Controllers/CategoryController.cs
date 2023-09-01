using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using test_v1.Api.Extensions;
using test_v1.Api.Requests;
using test_v1.Api.Responses;
using test_v1.Bll;
using test_v1.Bll.Services;

namespace test_v1.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CategoryController
{
    private IValidator<CreateCategoryRequest> _createCategoryValidator;

    private IValidator<UpdateCategoryRequest> _updateCategoryValidator;

    private readonly CategoryService _service;

    private readonly CategoryQueryService _queryService;


    public CategoryController(IValidator<CreateCategoryRequest> createCategoryValidator,
        IValidator<UpdateCategoryRequest> updateCategoryValidator,
        CategoryService service, CategoryQueryService queryService)
    {
        _createCategoryValidator = createCategoryValidator;
        _updateCategoryValidator = updateCategoryValidator;
        _service = service;
        _queryService = queryService;
    }

    [HttpPost("Create")]
    public Task<BaseApiResponse> Create([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var result = _createCategoryValidator.Validate(request);

        if (!result.IsValid)
        {
            throw new ValidationException("Request is not valid");
        }

        return _service.Create(request.ToCategoryModel(), cancellationToken);
    }

    [HttpPost("Update")]
    public Task<BaseApiResponse> Update([FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var result = _updateCategoryValidator.Validate(request);

        if (!result.IsValid)
        {
            throw new ValidationException("Request is not valid");
        }

        return _service.Update(request.ToCategoryModel(), cancellationToken);
    }

    [HttpGet("GetById/{id:int:min(0)}")]
    public async Task<CategoryResponse?> GetById([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var result = await _queryService.GetById(id, cancellationToken);

        return result?.ToCategoryResponse();
    }

    [HttpGet("GetChildrenById/{id:int:min(1)}")]
    public async Task<CategoryDetailedResponse?> GetChildrenById([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var result = await _queryService.GetChildrenById(id, cancellationToken);

        return result?.ToCategoryDetailedResponse();
    }
}
