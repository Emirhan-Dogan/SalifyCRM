using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Application.Responses.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Responses.CustomerPermissions
{
    public class CustomerPermissionDetailResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PermissionId { get; set; }

        public string Status { get; set; }

        public CustomerResponse Customer { get; set; }
        public PermissionResponse Permission { get; set; }
    }
}
