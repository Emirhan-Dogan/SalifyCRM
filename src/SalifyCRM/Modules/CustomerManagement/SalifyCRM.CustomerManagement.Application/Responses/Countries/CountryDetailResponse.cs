using SalifyCRM.CustomerManagement.Application.Responses.Cities;
using SalifyCRM.CustomerManagement.Application.Responses.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Responses.Countries
{
    public class CountryDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public List<CityResponse> Cities { get; set; }
        public List<DistrictResponse> Districts { get; set; }
    }
}
