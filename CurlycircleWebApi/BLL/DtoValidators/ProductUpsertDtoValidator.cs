using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Dtos;
using FluentValidation;

namespace BLL.DtoValidators
{
    public class ProductUpsertDtoValidator : AbstractValidator<ProductUpsertDto>
    {
        public ProductUpsertDtoValidator()
        {
            RuleFor(ent => ent.Name)
                .NotEmpty().WithMessage("Name is required");
            RuleFor(ent => ent.Price)
                .NotEmpty().WithMessage("Price is required.");
        }
    }
}
