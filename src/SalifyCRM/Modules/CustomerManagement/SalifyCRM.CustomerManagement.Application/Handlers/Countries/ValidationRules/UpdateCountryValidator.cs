using FluentValidation;
using SalifyCRM.CustomerManagement.Application.Handlers.Countries.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Countries.ValidationRules
{
    public class UpdateCountryValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryValidator()
        {
            
        }
    }
}
