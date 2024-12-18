using SalifyCRM.CustomerManagement.Application.Responses.Cities;
using SalifyCRM.CustomerManagement.Application.Responses.Countries;
using SalifyCRM.CustomerManagement.Application.Responses.Districts;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class DistrictExtensions
    {
        public static DistrictResponse ToDistrictResponse(this District district)
        {
            return new DistrictResponse()
            {
                CityId = district.CityId,
                Id = district.Id,
                Name = district.Name,
                Status = district.Status
            };
        }

        public static List<DistrictResponse> ToDistrictResponseList(this List<District> districts)
        {
            return districts.Select(d => new DistrictResponse()
            {
                CityId = d.CityId,
                Id = d.Id,
                Name = d.Name,
                Status = d.Status
            }).ToList();
        }

        public static DistrictDetailResponse ToDistrictDetailResponse(this District district)
        {
            return new DistrictDetailResponse()
            {
                Id = district.Id,
                Status = district.Status,
                Name = district.Name,
                CityId = district.CityId,
                City = CityExtensions.ToCityResponse(district.City),
                Country = CountryExtensions.ToCountryResponse(district.City.Country)
            };
        }

        public static List<DistrictDetailResponse> ToDistrictDetailResponseList(this List<District> districts)
        {
            return districts.Select(d => d.ToDistrictDetailResponse()).ToList();
        }
    }
}
