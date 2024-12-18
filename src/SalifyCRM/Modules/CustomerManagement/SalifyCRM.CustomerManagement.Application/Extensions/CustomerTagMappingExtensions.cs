using SalifyCRM.CustomerManagement.Application.Responses.CustomerTagMappings;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class CustomerTagMappingExtensions
    {
        public static CustomerTagMappingResponse ToCustomerTagMappingResponse(this CustomerTagMapping customerTagMapping)
        {
            return new CustomerTagMappingResponse()
            {
                Status = customerTagMapping.Status,
                CustomerId = customerTagMapping.CustomerId,
                Id = customerTagMapping.Id,
                CustomerTagId = customerTagMapping.CustomerTagId,
                Customer = CustomerExtensions.ToCustomerResponse(customerTagMapping.Customer),
                CustomerTag = CustomerTagExtensions.ToCustomerTagResponse(customerTagMapping.CustomerTag)
            };
        }

        public static List<CustomerTagMappingResponse> ToCustomerTagMappingResponseList(this List<CustomerTagMapping> customerTagMappings)
        {
            return customerTagMappings.Select(
                c => new CustomerTagMappingResponse()
                {
                    Status = c.Status,
                    CustomerId = c.CustomerId,
                    Id = c.Id,
                    CustomerTagId = c.CustomerTagId,
                    Customer = CustomerExtensions.ToCustomerResponse(c.Customer),
                    CustomerTag = CustomerTagExtensions.ToCustomerTagResponse(c.CustomerTag)
                }).ToList();
        }

        public static CustomerTagMappingDetailResponse ToCustomerTagMappingDetailResponse(this CustomerTagMapping customerTagMapping)
        {
            return new CustomerTagMappingDetailResponse()
            {
                Status = customerTagMapping.Status,
                CustomerId = customerTagMapping.CustomerId,
                Id = customerTagMapping.Id,
                CustomerTagId = customerTagMapping.CustomerTagId,
                Customer = CustomerExtensions.ToCustomerResponse(customerTagMapping.Customer),
                CustomerTag = CustomerTagExtensions.ToCustomerTagResponse(customerTagMapping.CustomerTag)
            };
        }

        public static List<CustomerTagMappingDetailResponse> ToCustomerTagMappingDetailResponseList(this List<CustomerTagMapping> customerTagMappings)
        {
            return customerTagMappings.Select(
                c => new CustomerTagMappingDetailResponse()
                {
                    Status = c.Status,
                    CustomerId = c.CustomerId,
                    Id = c.Id,
                    CustomerTagId = c.CustomerTagId,
                    Customer = CustomerExtensions.ToCustomerResponse(c.Customer),
                    CustomerTag = CustomerTagExtensions.ToCustomerTagResponse(c.CustomerTag)
                }).ToList();
        }
    }
}
