using FluentValidation;
using SalifyCRM.IdentityServer.Application.Handlers.Groups.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.Groups.ValidationRules
{
    public class CreateGroupValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupValidator()
        {
            RuleFor(group => group.CreatedUserId)
                .NotNull().WithMessage("CreatedUserId değeri null olamaz.")
                .GreaterThan(0).WithMessage("CreatedUserId değeri 0'dan büyük olmalıdır.");

            RuleFor(group => group.Name)
                .NotNull().WithMessage("Name değeri null olamaz.");

            RuleFor(group => group.Status)
                .NotNull().WithMessage("Status değeri null olamaz.");
        }
    }
}
