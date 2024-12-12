using SalifyCRM.CustomerManagement.Application.Responses.Addresses;
using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Responses.CustomerAddresses
{
    public class CustomerAddressDetailResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int AddressId { get; set; }

        public string Status { get; set; }

        public CustomerResponse Customer { get; set; }
        public AddressResponse Address { get; set; }
    }
}
