using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Responses.CustomerNotes
{
    public class CustomerNoteDetailResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public string Note { get; set; }
        public DateTime NoteDate { get; set; }

        public string Status { get; set; }

        public CustomerResponse Customer { get; set; }
    }
}
