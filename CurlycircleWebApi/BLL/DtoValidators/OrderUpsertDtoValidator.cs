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
                .NotEmpty().WithMessage("Name is required");
            RuleFor(ent => ent.City)
                .NotEmpty().WithMessage("City is required.");
            RuleFor(ent => ent.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required.");
            RuleFor(ent => ent.Line1)
                .NotEmpty().WithMessage("Line1 is required.");
            RuleFor(ent => ent.Email)
                .NotEmpty().WithMessage("Email is required.");
            RuleFor(ent => ent.OrderDateTime)
                .NotEmpty().WithMessage("OrderDateTime is required.");
            RuleFor(ent => ent.PaymentMethod)
                .NotEmpty().WithMessage("PaymentMethod is required.");
            RuleFor(ent => ent.ShippingMethod)
                .NotEmpty().WithMessage("ShippingMethod is required.");
            RuleFor(ent => ent.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required.");
        }
    }
}
