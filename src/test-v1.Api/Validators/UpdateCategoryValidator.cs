using FluentValidation;
using test_v1.Api.Consts;
using test_v1.Api.Requests;

namespace test_v1.Api.Validators;

internal sealed class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .GreaterThan(ConfigurationConsts.CategoryRootId)
            .LessThan(Int32.MaxValue);

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .Length(ConfigurationConsts.MinNameLength, ConfigurationConsts.MaxNameLength);

        RuleFor(x => x.ParentId)
            .NotNull()
            .GreaterThan(ConfigurationConsts.CategoryRootId - 1)
            .LessThan(Int32.MaxValue);

        RuleFor(x => x.ParentId).NotEqual(x => x.Id).WithMessage("ParentId must be different from Id");
    }
}