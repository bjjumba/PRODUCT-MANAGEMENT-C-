using FluentValidation;
using ProductManagement.Core.Entities;

namespace ProductManagement.API.Controllers
{
    public class Validation : AbstractValidator<UserAuth>
    {
        public Validation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password must be at least 8 characters");
            //RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required");
           // RuleFor(x => x.CountryCode).NotEmpty().WithMessage("CountryCode is required");
        }
    }
}
