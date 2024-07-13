using Data.DTOs.UserDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validations
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator(bool IsLogin = false,bool IsProfile = false)
        {
            if (IsLogin)
            {
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty");
                RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter a valid email address");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
                RuleFor(x => x.UserName).NotEmpty().WithMessage("Username cannot be empty");
            }
            else if (IsProfile)
            {
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty");
                RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter a valid email address");
                RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name cannot be empty");
                RuleFor(x => x.FullName).Length(4, 25).WithMessage("Full name must be between 4 and 25 characters")
                    .When(x => !string.IsNullOrEmpty(x.FullName));
            }
            else
            {
                RuleFor(x => x.UserName).NotEmpty().WithMessage("Username cannot be empty");
                RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name cannot be empty");
                RuleFor(x => x.FullName).Length(5, 50).WithMessage("Full name must be between 5 and 50 characters");
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty");
                RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter a valid email address");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
                RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters long")
                    .When(x => !string.IsNullOrEmpty(x.Password));
                RuleFor(x => x.Password).Must(x => x.Any(char.IsDigit)).WithMessage("Password must contain at least 1 digit")
                    .When(x => !string.IsNullOrEmpty(x.Password));
                RuleFor(x => x.Password).Must(x => x.Any(char.IsUpper)).WithMessage("Password must contain at least 1 uppercase letter")
                    .When(x => !string.IsNullOrEmpty(x.Password));
                RuleFor(x => x.Password).Must(x => x.Any(char.IsLower)).WithMessage("Password must contain at least 1 lowercase letter")
                    .When(x => !string.IsNullOrEmpty(x.Password));
                RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Passwords do not match")
                    .When(x => !string.IsNullOrEmpty(x.Password));
                RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Password confirmation cannot be empty");
            }
        }
    }
}
