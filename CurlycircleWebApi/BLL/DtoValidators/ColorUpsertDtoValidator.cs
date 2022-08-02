using BLL.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DtoValidators
{
    public class ColorUpsertDtoValidator : AbstractValidator<ColorUpsertDto>
    {
        public ColorUpsertDtoValidator()
        {
            RuleFor(ent => ent.Name)
               .NotEmpty().WithMessage("Name is required.");
        }
    }
}
