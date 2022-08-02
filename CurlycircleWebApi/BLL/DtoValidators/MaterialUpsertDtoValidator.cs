using BLL.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DtoValidators
{
    public class MaterialUpsertDtoValidator : AbstractValidator<MaterialUpsertDto>
    {
        public MaterialUpsertDtoValidator()
        {
            RuleFor(ent => ent.Name)
               .NotEmpty().WithMessage("Name is required.");
        }
    }
}
