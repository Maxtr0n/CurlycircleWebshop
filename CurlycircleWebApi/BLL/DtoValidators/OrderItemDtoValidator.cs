using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Dtos;
using Domain.Entities;
using FluentValidation;

namespace BLL.DtoValidators
{
    public class OrderItemDtoValidator : AbstractValidator<OrderItemUpsertDto>
    {
        public OrderItemDtoValidator()
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
