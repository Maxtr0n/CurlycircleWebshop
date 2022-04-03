using BLL.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DtoValidators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(ent => ent.Email)
                .NotEmpty().WithMessage("Email is required.");
            RuleFor(ent => ent.Password)
                .NotEmpty().WithMessage("Password is required.");
            RuleFor(ent => ent.FirstName)
                .NotEmpty().WithMessage("FirstName is required.");
            RuleFor(ent => ent.LastName)
                .NotEmpty().WithMessage("LastName is required.");
            RuleFor(ent => ent.City)
                .NotEmpty().WithMessage("City is required.");
            RuleFor(ent => ent.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required.");
            RuleFor(ent => ent.Line1)
                .NotEmpty().WithMessage("Line1 is required.");
            RuleFor(ent => ent.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required.");
        }
    }
}
