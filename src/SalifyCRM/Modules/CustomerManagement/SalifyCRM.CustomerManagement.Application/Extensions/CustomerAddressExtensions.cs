using SalifyCRM.CustomerManagement.Application.Responses.CustomerAddresses;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class CustomerAddressExtensions
    {
        public static CustomerAddressResponse ToCustomerAddressResponse(this CustomerAddress customerAddress)
        {
            return new CustomerAddressResponse()
            {
                Id = customerAddress.Id,
                CustomerId = customerAddress.CustomerId,
                AddressId = customerAddress.AddressId,
                Status = customerAddress.Status,
                Address = AddressExtensions.ToAddressResponse(customerAddress.Address),
                Customer = CustomerExtensions.ToCustomerResponse(customerAddress.Customer)
            };
        }

        public static List<CustomerAddressResponse> ToCustomerAddressResponseList(this List<CustomerAddress> customerAddresses)
        {
            return customerAddresses.Select(
                c=>new CustomerAddressResponse()
                {
                    Id = c.Id,
                    CustomerId = c.CustomerId,
                    AddressId = c.AddressId,
                    Status = c.Status,
                    Address = AddressExtensions.ToAddressResponse(c.Address),
                    Customer = CustomerExtensions.ToCustomerResponse(c.Customer)
                }).ToList();
        }

        public static CustomerAddressDetailResponse ToCustomerAddressDetailResponse(this CustomerAddress customerAddress)
        {
            return new CustomerAddressDetailResponse()
            {
                Id = customerAddress.Id,
                CustomerId = customerAddress.CustomerId,
                AddressId = customerAddress.AddressId,
                Status = customerAddress.Status,
                Address = AddressExtensions.ToAddressResponse(customerAddress.Address),
                Customer = CustomerExtensions.ToCustomerResponse(customerAddress.Customer)
            };
        }

        public static List<CustomerAddressDetailResponse> ToCustomerAddressDetailResponseList(this List<CustomerAddress> customerAddresses)
        {
            return customerAddresses.Select(
                c => new CustomerAddressDetailResponse()
                {
                    Id = c.Id,
                    CustomerId = c.CustomerId,
                    AddressId = c.AddressId,
                    Status = c.Status,
                    Address = AddressExtensions.ToAddressResponse(c.Address),
                    Customer = CustomerExtensions.ToCustomerResponse(c.Customer)
                }).ToList();
        }
    }
}
