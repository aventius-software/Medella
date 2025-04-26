using FluentValidation;
using Services.Shared.Validation;

namespace Module.Patient.Shared.Features.Patient;

public class PatientValidator : AbstractFluentValidationValidator<PatientRecord>
{
    public PatientValidator()
    {
        RuleFor(model => model.CommunityHealthIndexNumber)
            .NotNull()
            .WithMessage("Example error message for testing");        
    }
}
