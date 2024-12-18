using SalifyCRM.CustomerManagement.Application.Responses.Cities;
using SalifyCRM.CustomerManagement.Application.Responses.Countries;
using SalifyCRM.CustomerManagement.Application.Responses.Districts;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse()
            {
                Status = country.Status,
                Id = country.Id,
                Name = country.Name
            };
        }

        public static List<CountryResponse> ToCountryResponseList(this List<Country> countries)
        {
            return countries.Select(
                c=> new CountryResponse()
                {
                    Status = c.Status,
                    Id = c.Id,
                    Name = c.Name
                }).ToList();
        }

        public static CountryDetailResponse ToCountryDetailResponse(this Country country, List<City> cities)
        {
            return new CountryDetailResponse()
            {
                Id = country.Id,
                Status = country.Status,
                Name = country.Name,
                Cities = cities.Select(
                    c => new CityDetailResponse()
                    {
                        CountryId = c.Id,
                        Id = c.Id,
                        Name = c.Name,
                        Status = c.Status,
                        Districts = c.Districts.Select(
                            d => new DistrictResponse()
                            {
                                Id = d.Id,
                                Name = d.Name,
                                Status = d.Status,
                                CityId = d.CityId
                            }).ToList()
                    }).ToList()
            };
        }
    }
}
