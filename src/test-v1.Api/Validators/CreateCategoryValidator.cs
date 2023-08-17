using FluentValidation;
using test_v1.Api.Consts;
using test_v1.Api.Requests;

namespace test_v1.Api.Validators;

internal sealed class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .GreaterThan(ConfigurationConsts.CategoryRootId)
            .LessThan(Int32.MaxValue);

        RuleFor(x => x.Id).NotEqual(x => x.ParentId).WithMessage("Id must be different from ParentId");

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .Length(ConfigurationConsts.MinNameLength, ConfigurationConsts.MaxNameLength);

        RuleFor(x => x.ParentId)
            .NotNull()
            .GreaterThan(ConfigurationConsts.CategoryRootId - 1)
            .LessThan(Int32.MaxValue);
    }
}