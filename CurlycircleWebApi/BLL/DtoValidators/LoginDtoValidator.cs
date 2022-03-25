using BLL.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DtoValidators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(ent => ent.Email)
                .NotEmpty().WithMessage("Email is required.");
            RuleFor(ent => ent.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
