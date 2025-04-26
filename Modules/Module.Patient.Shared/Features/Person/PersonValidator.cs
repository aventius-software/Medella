using FluentValidation;
using Services.Shared.Validation;

namespace Module.Patient.Shared.Features.Person;

public class PersonValidator : AbstractFluentValidationValidator<PersonRecord>
{
    public PersonValidator()
    {
        RuleFor(model => model.NationalInsuranceNumber)
            .NotNull()
            .WithMessage("Example error message for testing");        
    }
}
