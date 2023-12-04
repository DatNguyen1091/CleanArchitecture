
using Application.Dto;
using FluentValidation;

namespace Application.FluentValidation
{
    public class SignUpValidation : AbstractValidator<SignUp>
    {
        public SignUpValidation()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("Not null")
                .MaximumLength(30).WithMessage("FirstName must not exceed 30 characters");

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage("Not null")
                .MaximumLength(30).WithMessage("LastName must not exceed 30 characters");

            RuleFor(user => user.Username)
                .NotEmpty().WithMessage("Not null")
                .MaximumLength(30).WithMessage("Username must not exceed 30 characters");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Not null")
                .MaximumLength(30).WithMessage("Password must not exceed 30 characters")
                .Must(password => password.Any(char.IsLetterOrDigit)).WithMessage("Password must contain letters and numbers!")
                .Must(password => password.Any(char.IsUpper)).WithMessage("Password must contain apital letters!")
                .Must(password => password.Any(ch => !char.IsLetterOrDigit(ch)))
                    .WithMessage("Password must contain special characters!");

            RuleFor(user => user.ConfirmPassword)
                .Equal(user => user.Password).WithMessage("Confirm password is incorrect!");

        }
    }
}
