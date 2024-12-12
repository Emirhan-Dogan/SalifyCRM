using FluentValidation;
using SalifyCRM.IdentityServer.Application.Handlers.UserGroups.Commands;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.UserGroups.ValidationRules
{
    public class CreateUserGroupValidator:AbstractValidator<CreateUserGroupCommand>
    {
        public CreateUserGroupValidator()
        {
            RuleFor(userGroup => userGroup.CreatedUserId)
                .NotNull().WithMessage("CreatedUserId değeri null olamaz.")
                .GreaterThan(0).WithMessage("CreatedUserId değeri 0'dan büyük olmalıdır.");

            RuleFor(userGroup => userGroup.GroupId)
                .NotNull().WithMessage("GroupId değeri null olamaz.")
                .GreaterThan(0).WithMessage("GroupId değeri 0'dan büyük olmalıdır.");

            RuleFor(userGroup => userGroup.UserId)
                .NotNull().WithMessage("UserId değeri null olamaz.")
                .GreaterThan(0).WithMessage("UserId değeri 0'dan büyük olmalıdır.");

            RuleFor(userGroup => userGroup.Status)
                .NotNull().WithMessage("Status değeri null olamaz.");
        }
    }
}
