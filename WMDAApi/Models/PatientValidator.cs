using FluentValidation;

namespace WMDAApi.Models
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            RuleFor(model => model.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(model => model.FirstName).MinimumLength(5).WithMessage("First name should be at least 5 characters long.");
            RuleFor(model => model.FirstName).Matches("^[a-zA-Z ]*$").WithMessage("First name allows only alphabet characters.");

            RuleFor(model => model.DateOfBirth).NotNull().WithMessage("Date of birth is required.");
        }
    }
}
