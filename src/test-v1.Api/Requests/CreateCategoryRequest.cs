﻿namespace test_v1.Api.Requests;

public sealed class CreateCategoryRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ParentId { get; set; } 
}
