using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class CustomerExtensions
    {
        public static CustomerResponse ToCustomerResponse(this Customer customer)
        {
            return new CustomerResponse()
            {
                BirthDate = customer.BirthDate,
                CustomerCategoryId = customer.CustomerCategoryId,
                CustomerCode = customer.CustomerCode,
                CustomerTypeId = customer.CustomerTypeId,
                Email = customer.Email,
                FirstName = customer.FirstName,
                Gender = customer.Gender,
                Id = customer.Id,
                IncomeLevel = customer.IncomeLevel,
                LastInteractionDate = customer.LastInteractionDate,
                LastName = customer.LastName,
                MartialStatusId = customer.MartialStatusId,
                OccupationId = customer.OccupationId,
                PhoneNumber = customer.PhoneNumber,
                Status = customer.Status,
                LastLoginDate = customer.LastLoginDate
            };
        }

        public static List<CustomerResponse> ToCustomerResponseList(this List<Customer> customers)
        {
            return customers.Select(
                c => new CustomerResponse()
                {
                    Email = c.Email,
                    FirstName = c.FirstName,
                    Gender = c.Gender,
                    Id = c.Id,
                    BirthDate = c.BirthDate,
                    CustomerCategoryId = c.CustomerCategoryId,
                    CustomerCode = c.CustomerCode,
                    CustomerTypeId = c.CustomerTypeId,
                    LastInteractionDate = c.LastInteractionDate,
                    LastName = c.LastName,
                    IncomeLevel = c.IncomeLevel,
                    LastLoginDate = c.LastLoginDate,
                    MartialStatusId = c.MartialStatusId,
                    OccupationId = c.OccupationId,
                    PhoneNumber = c.PhoneNumber,
                    Status = c.Status
                }).ToList();
        }

        public static CustomerDetailResponse ToCustomerDetailResponse(
            this Customer customer,
            List<Address> addresses,
            List<CustomerSocial> customerSocials,
            List<CustomerPermission> customerPermissions,
            List<CustomerNote> customerNotes,
            List<CustomerTag> customerTags)
        {
            return new CustomerDetailResponse()
            {
                Status = customer.Status,
                OccupationId = customer.OccupationId,
                BirthDate = customer.BirthDate,
                CustomerCategoryId = customer.CustomerCategoryId,
                CustomerCode = customer.CustomerCode,
                CustomerTypeId = customer.CustomerTypeId,
                Email = customer.Email,
                FirstName = customer.FirstName,
                Id = customer.Id,
                LastName = customer.LastName,
                Gender = customer.Gender,
                IncomeLevel = customer.IncomeLevel,
                PhoneNumber = customer.PhoneNumber,
                MartialStatusId = customer.MartialStatusId,
                LastInteractionDate = customer.LastInteractionDate,
                LastLoginDate = customer.LastLoginDate,

                CustomerCategory = CustomerCategoryExtensions.ToCustomerCategoryResponse(customer.CustomerCategory),
                CustomerType = CustomerTypeExtensions.ToCustomerTypeResponse(customer.CustomerType),
                Occupation = OccupationExtensions.ToOccupationResponse(customer.Occupation),
                MartialStatus = MartialStatusExtensions.ToMartialStatusResponse(customer.MartialStatus),

                Addresses = AddressExtensions.ToAddressResponseList(addresses),
                CustomerNotes = CustomerNoteExtensions.ToCustomerNoteResponseList(customerNotes),
                CustomerPermissions = CustomerPermissionExtensions.ToCustomerPermissionResponseList(customerPermissions),
                CustomerSocials = CustomerSocialExtensions.ToCustomerSocialResponseList(customerSocials),
                CustomerTags = CustomerTagExtensions.ToCustomerTagResponseList(customerTags),
            };
        }
    }
}
