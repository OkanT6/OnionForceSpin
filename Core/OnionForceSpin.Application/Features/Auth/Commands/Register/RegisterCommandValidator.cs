using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator:AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidator()
        {
            RuleFor(X => X.FullName)
                .NotEmpty().WithMessage("Full Name is required")
                .MinimumLength(2).WithMessage("Full Name must be at least 2 characters long")
                .MaximumLength(50).WithMessage("Full Name must not exceed 50 characters")
                .WithName("Full Name");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .WithName("Email");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
                .MaximumLength(20).WithMessage("Password must not exceed 20 characters")
                .WithName("Password");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required")
                .Equal(x => x.Password).WithMessage("Password and Confirm Password do not match")
                .WithName("Confirm Password");
        }
    }
}
