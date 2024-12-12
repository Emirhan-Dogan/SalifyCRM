using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Responses.Occupations
{
    public class OccupationDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }
        public string Status { get; set; }

        public List<CustomerResponse> Customers { get; set; }
    }
}
