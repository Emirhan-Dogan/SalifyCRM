using SalifyCRM.CustomerManagement.Application.Responses.Cities;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class CityExtensions
    {
        public static CityResponse ToCityResponse(this City city)
        {
            return new CityResponse()
            {
                Name = city.Name,
                CountryId = city.CountryId,
                Id = city.Id,
                Status = city.Status
            };
        }

        public static List<CityResponse> ToCityResponseList(this List<City> cities)
        {
            return cities.Select(
                c=> new CityResponse()
                {
                    Name = c.Name,
                    CountryId = c.CountryId,
                    Id = c.Id,
                    Status = c.Status
                }).ToList();
        }

        public static CityDetailResponse ToCityDetailResponse(this City city, List<District> districts)
        {
            return new CityDetailResponse()
            {
                Status = city.Status,
                Id = city.Id,
                CountryId = city.CountryId,
                Name = city.Name,
                Country = CountryExtensions.ToCountryResponse(city.Country),
                Districts = DistrictExtensions.ToDistrictResponseList(districts)
            };
        }
    }
}
