using FluentValidation;
using SalifyCRM.IdentityServer.Application.Handlers.UserOperationClaims.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.UserOperationClaims.ValidationRules
{
    public class CreateUserOperationClaimValidator : AbstractValidator<CreateUserOperationClaimCommand>
    {
        public CreateUserOperationClaimValidator()
        {
            RuleFor(userOperationClaim => userOperationClaim.CreatedUserId)
                .NotNull().WithMessage("CreatedUserId değeri null olamaz.")
                .GreaterThan(0).WithMessage("CreatedUserId değeri 0'dan büyük olmalıdır.");

            RuleFor(userOperationClaim => userOperationClaim.UserId)
                .NotNull().WithMessage("UserId değeri null olamaz.")
                .GreaterThan(0).WithMessage("UserId değeri 0'dan büyük olmalıdır.");

            RuleFor(userOperationClaim => userOperationClaim.OperationClaimId)
                .NotNull().WithMessage("OperationClaimId değeri null olamaz.")
                .GreaterThan(0).WithMessage("OperationClaimId değeri 0'dan büyük olmalıdır.");

            RuleFor(userOperationClaim => userOperationClaim.Status)
                .NotNull().WithMessage("Status değeri null olamaz.");
        }
    }
}
