using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Dtos;
using FluentValidation;

namespace BLL.DtoValidators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(ent => ent.Username)
                .NotEmpty().WithMessage("Username is required.");
            RuleFor(ent => ent.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
