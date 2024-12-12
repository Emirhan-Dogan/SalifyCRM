using FluentValidation;
using SalifyCRM.IdentityServer.Application.Handlers.GroupClaims.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.GroupClaims.ValidationRules
{
    public class CreateGroupClaimValidator : AbstractValidator<CreateGroupClaimCommand>
    {
        public CreateGroupClaimValidator()
        {
            RuleFor(groupClaim => groupClaim.CreatedUserId)
                .NotNull().WithMessage("CreatedUserId değeri null olamaz.")
                .GreaterThan(0).WithMessage("CreatedUserId değeri 0'dan büyük olmalıdır.");

            RuleFor(groupClaim => groupClaim.GroupId)
                .NotNull().WithMessage("GroupId değeri null olamaz.")
                .GreaterThan(0).WithMessage("GroupId değeri 0'dan büyük olmalıdır.");

            RuleFor(groupClaim => groupClaim.OperationClaimId)
                .NotNull().WithMessage("OperationClaimId değeri null olamaz.")
                .GreaterThan(0).WithMessage("OperationClaimId değeri 0'dan büyük olmalıdır.");

            RuleFor(groupClaim => groupClaim.Status)
                .NotNull().WithMessage("Status değeri null olamaz.");
        }
    }
}
