using SalifyCRM.CustomerManagement.Application.Responses.CustomerPermissions;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class CustomerPermissionExtensions
    {
        public static CustomerPermissionResponse ToCustomerPermissionResponse(this CustomerPermission customerPermission)
        {
            return new CustomerPermissionResponse()
            {
                CustomerId = customerPermission.CustomerId,
                PermissionId = customerPermission.PermissionId,
                Status = customerPermission.Status,
                Id = customerPermission.Id,
                Customer = CustomerExtensions.ToCustomerResponse(customerPermission.Customer),
                Permission = PermissionExtensions.ToPermissionResponse(customerPermission.Permission)
            };
        }

        public static List<CustomerPermissionResponse> ToCustomerPermissionResponseList(this List<CustomerPermission> customerPermissions)
        {
            return customerPermissions.Select(
                c => new CustomerPermissionResponse()
                {
                    CustomerId = c.CustomerId,
                    PermissionId = c.PermissionId,
                    Status = c.Status,
                    Id = c.Id,
                    Customer = CustomerExtensions.ToCustomerResponse(c.Customer),
                    Permission = PermissionExtensions.ToPermissionResponse(c.Permission)
                }).ToList();
        }

        public static CustomerPermissionDetailResponse ToCustomerPermissionDetailResponse(this CustomerPermission customerPermission)
        {
            return new CustomerPermissionDetailResponse()
            {
                CustomerId = customerPermission.CustomerId,
                PermissionId = customerPermission.PermissionId,
                Status = customerPermission.Status,
                Id = customerPermission.Id,
                Customer = CustomerExtensions.ToCustomerResponse(customerPermission.Customer),
                Permission = PermissionExtensions.ToPermissionResponse(customerPermission.Permission)
            };
        }

        public static List<CustomerPermissionDetailResponse> ToCustomerPermissionDetailResponseList(this List<CustomerPermission> customerPermissions)
        {
            return customerPermissions.Select(
               c => new CustomerPermissionDetailResponse()
               {
                   CustomerId = c.CustomerId,
                   PermissionId = c.PermissionId,
                   Status = c.Status,
                   Id = c.Id,
                   Customer = CustomerExtensions.ToCustomerResponse(c.Customer),
                   Permission = PermissionExtensions.ToPermissionResponse(c.Permission)
               }).ToList();
        }
    }
}
