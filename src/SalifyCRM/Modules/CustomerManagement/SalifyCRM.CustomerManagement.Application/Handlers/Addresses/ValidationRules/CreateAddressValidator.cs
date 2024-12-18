using FluentValidation;
using SalifyCRM.CustomerManagement.Application.Handlers.Addresses.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Addresses.ValidationRules
{
    public class CreateAddressValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressValidator()
        {
            
        }
    }
}
