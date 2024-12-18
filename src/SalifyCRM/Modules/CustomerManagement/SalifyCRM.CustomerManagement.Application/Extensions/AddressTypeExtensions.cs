using SalifyCRM.CustomerManagement.Application.Responses.AddressTypes;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class AddressTypeExtensions
    {
        public static AddressTypeResponse ToAddressTypeResponse(this AddressType addressType)
        {
            return new AddressTypeResponse()
            {
                Id = addressType.Id,
                Descriptions = addressType.Descriptions,
                Name = addressType.Name,
                Status = addressType.Status
            };
        }

        public static List<AddressTypeResponse> ToAddressTypeResponseList(this List<AddressType> addressTypes)
        {
            return addressTypes.Select(
                c => new AddressTypeResponse()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Status = c.Status,
                    Descriptions = c.Descriptions
                }).ToList();
        }
    }
}
