using FluentValidation;
using SalifyCRM.IdentityServer.Application.Handlers.Users.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.Users.ValidationRules
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(user => user.Id)
                .NotNull().WithMessage("Id değeri null olamaz.")
                .GreaterThan(0).WithMessage("Id negatif olamaz.");

            RuleFor(user => user.LastUpdatedUserId)
                .NotNull().WithMessage("LastUpdatedUserId değeri null olamaz.")
                .GreaterThan(0).WithMessage("LastUpdatedUserId negatif olamaz.");

            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("FirstName alanı boş olamaz.");

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage("LastName alanı boş olamaz.");

            RuleFor(user => user.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber alanı boş olamaz.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email alanı boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir Email adresi giriniz.");

            RuleFor(user => user.Gender)
                .NotEmpty().WithMessage("Gender alanı boş olamaz.");

            RuleFor(user => user.Status)
                .NotNull().WithMessage("Status alanı boş olamaz.");
        }
    }
}
