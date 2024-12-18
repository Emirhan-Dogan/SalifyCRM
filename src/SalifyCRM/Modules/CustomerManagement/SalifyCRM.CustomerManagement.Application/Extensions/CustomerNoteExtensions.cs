using SalifyCRM.CustomerManagement.Application.Responses.CustomerNotes;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class CustomerNoteExtensions
    {
        public static CustomerNoteResponse ToCustomerNoteResponse(this CustomerNote customerNote)
        {
            return new CustomerNoteResponse()
            {
                CustomerId = customerNote.CustomerId,
                Id = customerNote.Id,
                Note = customerNote.Note,
                NoteDate = customerNote.NoteDate,
                Status = customerNote.Status,
                Customer = CustomerExtensions.ToCustomerResponse(customerNote.Customer)
            };
        }

        public static List<CustomerNoteResponse> ToCustomerNoteResponseList(this List<CustomerNote> customerNotes)
        {
            return customerNotes.Select(
                c => new CustomerNoteResponse()
                {
                    CustomerId = c.CustomerId,
                    Id = c.Id,
                    Note = c.Note,
                    NoteDate = c.NoteDate,
                    Status = c.Status,
                    Customer = CustomerExtensions.ToCustomerResponse(c.Customer)
                }).ToList();
        }

        public static CustomerNoteDetailResponse ToCustomerNoteDetailResponse(this CustomerNote customerNote)
        {
            return new CustomerNoteDetailResponse()
            {
                CustomerId = customerNote.CustomerId,
                Id = customerNote.Id,
                Note = customerNote.Note,
                NoteDate = customerNote.NoteDate,
                Status = customerNote.Status,
                Customer = CustomerExtensions.ToCustomerResponse(customerNote.Customer)
            };
        }

        public static List<CustomerNoteDetailResponse> ToCustomerNoteDetailResponseList(this List<CustomerNote> customerNotes)
        {
            return customerNotes.Select(
                c => new CustomerNoteDetailResponse()
                {
                    CustomerId = c.CustomerId,
                    Id = c.Id,
                    Note = c.Note,
                    NoteDate = c.NoteDate,
                    Status = c.Status,
                    Customer = CustomerExtensions.ToCustomerResponse(c.Customer)
                }).ToList();
        }
    }
}
