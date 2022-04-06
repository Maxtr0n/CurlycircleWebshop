using BLL.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DtoValidators
{
    public class RevokeDtoValidator : AbstractValidator<RevokeDto>
    {
        public RevokeDtoValidator()
        {
            RuleFor(ent => ent.Id)
                .NotEmpty().WithMessage("Id is required.");
            RuleFor(ent => ent.Email)
                .NotEmpty().WithMessage("Email is required.");
        }
    }
}
