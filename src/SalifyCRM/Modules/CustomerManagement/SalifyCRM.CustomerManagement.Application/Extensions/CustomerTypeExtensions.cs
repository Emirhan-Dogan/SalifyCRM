using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTypes;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class CustomerTypeExtensions
    {
        public static CustomerTypeResponse ToCustomerTypeResponse(this CustomerType customerType)
        {
            return new CustomerTypeResponse()
            {
                Id = customerType.Id,
                Status = customerType.Status,
                Descriptions = customerType.Descriptions,
                Name = customerType.Name
            };
        }

        public static List<CustomerTypeResponse> ToCustomerTypeResponseList(this List<CustomerType> customerTypes)
        {
            return customerTypes.Select(
                c => new CustomerTypeResponse()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Status = c.Status,
                    Descriptions = c.Descriptions
                }).ToList();
        }

        public static CustomerTypeDetailResponse ToCustomerTypeDetailResponse(this CustomerType customerType, List<CustomerResponse> customers)
        {
            return new CustomerTypeDetailResponse()
            {
                Id = customerType.Id,
                Name = customerType.Name,
                Descriptions = customerType.Descriptions,
                Status = customerType.Status,
                Customers = customers
            };
        }
    }
}
