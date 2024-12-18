using SalifyCRM.CustomerManagement.Application.Responses.CustomerCategories;
using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class CustomerCategoryExtensions
    {
        public static CustomerCategoryResponse ToCustomerCategoryResponse(this CustomerCategory customerCategory)
        {
            return new CustomerCategoryResponse()
            {
                Id = customerCategory.Id,
                Name = customerCategory.Name,
                Status = customerCategory.Status
            };
        }

        public static List<CustomerCategoryResponse> ToCustomerCategoryResponseList(this List<CustomerCategory> customerCategories)
        {
            return customerCategories.Select(
                c => new CustomerCategoryResponse()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Status = c.Status
                }).ToList();
        }

        public static CustomerCategoryDetailResponse ToCustomerCategoryDetailResponse(this CustomerCategory customerCategory, List<CustomerResponse> customers)
        {
            return new CustomerCategoryDetailResponse()
            {
                Id = customerCategory.Id,
                Name = customerCategory.Name,
                Descriptions = customerCategory.Descriptions,
                Status = customerCategory.Status,
                Customers = customers
            };
        }
    }
}
