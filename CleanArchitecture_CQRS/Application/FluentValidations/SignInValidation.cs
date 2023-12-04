
using Application.Dto;
using FluentValidation;

namespace Application.FluentValidations
{
    public class SignInValidation : AbstractValidator<SignIn>
    {
        public SignInValidation()
        {
            RuleFor(user => user.Username)
                .NotEmpty().WithMessage("Not null")
                .MaximumLength(30).WithMessage("Username must not exceed 30 characters");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Not null")
                .MaximumLength(30).WithMessage("Password must not exceed 30 characters");
        }
    }
}
