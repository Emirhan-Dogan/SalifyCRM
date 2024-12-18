using SalifyCRM.CustomerManagement.Application.Responses.Addresses;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class AddressExtensions
    {
        public static AddressResponse ToAddressResponse(this Address address)
        {
            return new AddressResponse()
            {
                AddressTypeId = address.AddressTypeId,
                CityId = address.CityId,
                CountryId = address.CountryId,
                Details = address.Details,
                DistrictId = address.DistrictId,
                Id = address.Id,
                Name = address.Name,
                Status = address.Status
            };
        }

        public static List<AddressResponse> ToAddressResponseList(this List<Address> addresses)
        {
            return addresses.Select(
                a => new AddressResponse()
                {
                    AddressTypeId = a.AddressTypeId,
                    CityId = a.CityId,
                    CountryId = a.CountryId,
                    Details = a.Details,
                    DistrictId = a.DistrictId,
                    Id = a.Id,
                    Name = a.Name,
                    Status = a.Status
                }).ToList();
        }

        public static AddressDetailResponse ToAddressDetailResponse(this Address address)
        {
            return new AddressDetailResponse()
            {
                AddressTypeId = address.AddressTypeId,
                CityId = address.CityId,
                CountryId = address.CountryId,
                Details = address.Details,
                DistrictId = address.DistrictId,
                Id = address.Id,
                Name = address.Name,
                Status = address.Status,
                AddressType = AddressTypeExtensions.ToAddressTypeResponse(address.AddressType),
                City = CityExtensions.ToCityResponse(address.City),
                Country = CountryExtensions.ToCountryResponse(address.Country),
                District = DistrictExtensions.ToDistrictResponse(address.District)
            };
        }

        public static List<AddressDetailResponse> ToAddressDetailResponseList(this List<Address> addresses)
        {
            return addresses.Select(
                a => new AddressDetailResponse()
                {
                    AddressTypeId = a.AddressTypeId,
                    CityId = a.CityId,
                    CountryId = a.CountryId,
                    Details = a.Details,
                    DistrictId = a.DistrictId,
                    Id = a.Id,
                    Name = a.Name,
                    Status = a.Status,
                    AddressType = AddressTypeExtensions.ToAddressTypeResponse(a.AddressType),
                    City = CityExtensions.ToCityResponse(a.City),
                    Country = CountryExtensions.ToCountryResponse(a.Country),
                    District = DistrictExtensions.ToDistrictResponse(a.District)
                }).ToList();
        }
    }
}
