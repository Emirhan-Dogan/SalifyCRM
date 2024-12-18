using SalifyCRM.CustomerManagement.Application.Responses.Countries;
using SalifyCRM.CustomerManagement.Application.Responses.Districts;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Responses.Cities
{
    public class CityDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public string Status { get; set; }

        public CountryResponse Country { get; set; }
        public List<DistrictResponse> Districts { get; set; }
    }
}
