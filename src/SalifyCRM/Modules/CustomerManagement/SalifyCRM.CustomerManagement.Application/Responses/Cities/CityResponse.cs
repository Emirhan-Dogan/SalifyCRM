using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Responses.Cities
{
    public class CityResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public string Status { get; set; }
    }
}
