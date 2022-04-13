using BLL.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DtoValidators
{
    public class OrderUpsertDtoValidator : AbstractValidator<OrderUpsertDto>
    {
        public OrderUpsertDtoValidator()
        {
            RuleFor(ent => ent.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(40);
            RuleFor(ent => ent.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(40);
            RuleFor(ent => ent.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required.")
                .MaximumLength(40);
            RuleFor(ent => ent.Line1)
                .NotEmpty().WithMessage("Line1 is required.")
                .MaximumLength(40);
            RuleFor(ent => ent.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(40);
            RuleFor(ent => ent.CartId)
                .NotEmpty().WithMessage("CartId is required.");
            RuleFor(ent => ent.PaymentMethod)
                .NotEmpty().WithMessage("PaymentMethod is required.");
            RuleFor(ent => ent.ShippingMethod)
                .NotEmpty().WithMessage("ShippingMethod is required.");
            RuleFor(ent => ent.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required.")
                .MaximumLength(40);
        }
    }
}
