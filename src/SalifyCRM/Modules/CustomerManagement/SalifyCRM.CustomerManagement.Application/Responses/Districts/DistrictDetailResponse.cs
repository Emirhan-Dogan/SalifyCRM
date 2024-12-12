using SalifyCRM.CustomerManagement.Application.Responses.Cities;
using SalifyCRM.CustomerManagement.Application.Responses.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Responses.Districts
{
    public class DistrictDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }

        public string Status { get; set; }

        public CityResponse City { get; set; }
        public CountryResponse Country { get; set; }
    }
}
