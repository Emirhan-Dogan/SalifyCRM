using FluentValidation;
using SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.Commands;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.ValidationRules
{
    public class CreateSocialMediaValidator : AbstractValidator<CreateSocialMediaCommand>
    {
        public CreateSocialMediaValidator()
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

            // Icon: Zorunlu alan
            RuleFor(x => x.Icon)
                .NotEmpty()
                .WithMessage("Icon is required.")
                 .MaximumLength(500)
                .WithMessage("Icon cannot exceed 500 characters.");

            // Status: Zorunlu ve 50 karakter sınırı
            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status is required.")
                .MaximumLength(50)
                .WithMessage("Status cannot exceed 50 characters.");
        }
    }
}
