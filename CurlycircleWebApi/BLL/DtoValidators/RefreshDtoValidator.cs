using BLL.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DtoValidators
{
    public class RefreshDtoValidator : AbstractValidator<RefreshDto>
    {
        public RefreshDtoValidator()
        {
            RuleFor(ent => ent.Email)
                .NotEmpty().WithMessage("Email is required.");
            RuleFor(ent => ent.Id)
                .NotEmpty().WithMessage("Id is required.");
            RuleFor(ent => ent.RefreshToken)
                .NotEmpty().WithMessage("RefreshToken is required.");
            RuleFor(ent => ent.AccessToken)
                .NotEmpty().WithMessage("AccessToken is required.");
        }
    }
}
