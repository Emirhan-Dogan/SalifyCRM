using FluentValidation;
using SalifyCRM.CustomerManagement.Application.Handlers.Districts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Districts.ValidationRules
{
    public class CreateDistrictValidator : AbstractValidator<CreateDistrictCommand>
    {
        public CreateDistrictValidator()
        {
            
        }
    }
}
