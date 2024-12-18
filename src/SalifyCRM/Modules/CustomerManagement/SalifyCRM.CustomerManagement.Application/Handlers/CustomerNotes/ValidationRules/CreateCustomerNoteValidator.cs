using FluentValidation;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerNotes.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerNotes.ValidationRules
{
    public class CreateCustomerNoteValidator : AbstractValidator<CreateCustomerNoteCommand>
    {
        public CreateCustomerNoteValidator()
        {
            
        }
    }
}
