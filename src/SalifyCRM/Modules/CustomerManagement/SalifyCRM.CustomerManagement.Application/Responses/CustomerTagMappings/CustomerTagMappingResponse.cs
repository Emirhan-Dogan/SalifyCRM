using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Responses.CustomerTagMappings
{
    public class CustomerTagMappingResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CustomerTagId { get; set; }

        public string Status { get; set; }

        public CustomerResponse Customer { get; set; }
        public CustomerTagResponse CustomerTag { get; set; }
    }
}
