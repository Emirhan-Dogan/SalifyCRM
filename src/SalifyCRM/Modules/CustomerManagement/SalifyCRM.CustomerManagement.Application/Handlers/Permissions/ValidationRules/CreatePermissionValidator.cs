using FluentValidation;
using SalifyCRM.CustomerManagement.Application.Handlers.Permissions.Commands;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Permissions.ValidationRules
{
    public class CreatePermissionValidator : AbstractValidator<CreatePermissionCommand>
    {
        public CreatePermissionValidator()
        {
            // CreatedUserId
            RuleFor(x => x.CreatedUserId)
                .GreaterThan(0)
                .WithMessage("The provided value should not be negative.");

            // Name: Zorunlu ve 50 karakter sınırı
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(50)
                .WithMessage("Name cannot exceed 50 characters.");

            // DocumentPath: Zorunlu alan
            RuleFor(x => x.DocumentPath)
                .NotEmpty()
                .WithMessage("DocumentPath is required.")
                 .MaximumLength(500)
                .WithMessage("DocumentPath cannot exceed 500 characters.");

            // Status: Zorunlu ve 50 karakter sınırı
            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status is required.")
                .MaximumLength(50)
                .WithMessage("Status cannot exceed 50 characters.");

            // Descriptions: Zorunlu alan
            RuleFor(x => x.Descriptions)
                 .MaximumLength(500)
                .WithMessage("Descriptions cannot exceed 500 characters.");
        }
    }
}
