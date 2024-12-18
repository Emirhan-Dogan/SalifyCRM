using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTags;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class CustomerTagExtensions
    {
        public static CustomerTagResponse ToCustomerTagResponse(this CustomerTag customerTag)
        {
            return new CustomerTagResponse()
            {
                Id = customerTag.Id,
                Name = customerTag.Name,
                Descriptions = customerTag.Descriptions,
                Status = customerTag.Status
            };
        }

        public static List<CustomerTagResponse> ToCustomerTagResponseList(this List<CustomerTag> customerTags)
        {
            return customerTags.Select(
                c => new CustomerTagResponse()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Descriptions = c.Descriptions,
                    Status = c.Status
                }).ToList();
        }

        public static CustomerTagDetailResponse ToCustomerTagDetailResponse(this CustomerTag customerTag, List<CustomerResponse> customers)
        {
            return new CustomerTagDetailResponse()
            {
                Id = customerTag.Id,
                Name = customerTag.Name,
                Descriptions = customerTag.Descriptions,
                Status = customerTag.Status,
                Customers = customers
            };
        }
    }
}
