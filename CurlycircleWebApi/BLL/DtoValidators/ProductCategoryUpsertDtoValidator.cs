using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Dtos;
using FluentValidation;

namespace BLL.DtoValidators
{
    public class ProductCategoryUpsertDtoValidator : AbstractValidator<ProductCategoryUpsertDto>
    {
        public ProductCategoryUpsertDtoValidator()
        {
            RuleFor(ent => ent.Name)
                .NotEmpty().WithMessage("Name is required");
        }
    }
}
