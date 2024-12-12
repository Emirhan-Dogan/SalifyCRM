using SalifyCRM.CustomerManagement.Application.Responses.AddressTypes;
using SalifyCRM.CustomerManagement.Application.Responses.Cities;
using SalifyCRM.CustomerManagement.Application.Responses.Countries;
using SalifyCRM.CustomerManagement.Application.Responses.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Responses.Addresses
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public int AddressTypeId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }

        public string Name { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }

    }
}
