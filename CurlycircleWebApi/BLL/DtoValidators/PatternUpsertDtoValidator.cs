using BLL.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DtoValidators
{
    public class PatternUpsertDtoValidator : AbstractValidator<PatternUpsertDto>
    {
        public PatternUpsertDtoValidator()
        {
            RuleFor(ent => ent.Name)
               .NotEmpty().WithMessage("Name is required.");
        }
    }
}
