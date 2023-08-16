using FluentValidation;
using test_v1.Bll.Models.Requests;

namespace test_v1.Bll.Validators;

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

        RuleFor(x => x.Id).NotEqual(x => x.ParentId).WithMessage("ParentId must be different from Id");
    }
}