using BLL.Dtos;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DtoValidators
{
    public class OrderItemUpsertDtoValidator : AbstractValidator<OrderItemUpsertDto>
    {
        public OrderItemUpsertDtoValidator()
        {
            RuleFor(ent => ent.OrderId)
                .NotEmpty().WithMessage("OrderId is required");
            RuleFor(ent => ent.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");
            RuleFor(ent => ent.Quantity)
                .NotEmpty().WithMessage("Quantity is required.");
        }
    }
}
