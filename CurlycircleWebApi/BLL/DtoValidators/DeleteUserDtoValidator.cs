using BLL.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DtoValidators
{
    public class DeleteUserDtoValidator : AbstractValidator<DeleteUserDto>
    {
        public DeleteUserDtoValidator()
        {
            RuleFor(ent => ent.Id)
               .NotEmpty().WithMessage("Id is required.");
            RuleFor(ent => ent.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
