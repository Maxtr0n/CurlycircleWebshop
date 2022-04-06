using BLL.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DtoValidators
{
    public class CartItemUpsertDtoValidator : AbstractValidator<CartItemUpsertDto>
    {
        public CartItemUpsertDtoValidator()
        {
            RuleFor(ent => ent.CartId)
               .NotEmpty().WithMessage("CartId is required.");
            RuleFor(ent => ent.Price)
                .NotEmpty().WithMessage("Price is required.");
            RuleFor(ent => ent.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");
            RuleFor(ent => ent.Quantity)
                .NotEmpty().WithMessage("Quantity is required.");
        }
    }
}
