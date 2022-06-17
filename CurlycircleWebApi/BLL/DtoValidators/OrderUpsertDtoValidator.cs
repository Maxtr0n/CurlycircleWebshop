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
            RuleFor(ent => ent.FirstName)
                .NotEmpty().WithMessage("FirstName is required")
                .MaximumLength(50);
            RuleFor(ent => ent.LastName)
                .NotEmpty().WithMessage("LastName is required")
                .MaximumLength(50);
            RuleFor(ent => ent.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50);
            RuleFor(ent => ent.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required.")
                .MaximumLength(50);
            RuleFor(ent => ent.Line1)
                .NotEmpty().WithMessage("Line1 is required.")
                .MaximumLength(50);
            RuleFor(ent => ent.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(50);
            RuleFor(ent => ent.CartId)
                .NotEmpty().WithMessage("CartId is required.");
            RuleFor(ent => ent.PaymentMethod)
                .IsInEnum().WithMessage("Not a valid value for PaymentMethod.");
            RuleFor(ent => ent.ShippingMethod)
                .IsInEnum().WithMessage("Not a valid value for ShippingMethod.");
            RuleFor(ent => ent.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required.")
                .MaximumLength(50);
        }
    }
}
