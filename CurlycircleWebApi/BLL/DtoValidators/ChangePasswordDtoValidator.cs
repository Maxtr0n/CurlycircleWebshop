using BLL.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DtoValidators
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(ent => ent.Email)
                .NotEmpty().WithMessage("Email is required.");
            RuleFor(ent => ent.NewPassword)
                .NotEmpty().WithMessage("NewPassword is required.");
            RuleFor(ent => ent.OldPassword)
               .NotEmpty().WithMessage("OldPassword is required.");
        }
    }
}
