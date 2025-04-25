using FluentValidation;

namespace Services.Shared.Validation;

public abstract class AbstractFluentValidationValidator<TModel> : AbstractValidator<TModel> where TModel : class
{
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<TModel>.CreateWithOptions((TModel)model, x => x.IncludeProperties(propertyName)));

        if (result.IsValid) return [];

        return result.Errors.Select(failure => failure.ErrorMessage);
    };
}
