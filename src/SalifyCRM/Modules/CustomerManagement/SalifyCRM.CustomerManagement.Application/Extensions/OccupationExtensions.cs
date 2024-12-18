using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Application.Responses.Occupations;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class OccupationExtensions
    {
        public static OccupationResponse ToOccupationResponse(this Occupation occupation)
        {
            return new OccupationResponse()
            {
                Id = occupation.Id,
                Name = occupation.Name,
                Descriptions = occupation.Descriptions,
                Status = occupation.Status
            };
        }

        public static List<OccupationResponse> ToOccupationResponseList(this List<Occupation> occupations)
        {
            return occupations.Select(
                o => new OccupationResponse()
                {
                    Id = o.Id,
                    Name = o.Name,
                    Status = o.Status,
                    Descriptions = o.Descriptions
                }).ToList();
        }

        public static OccupationDetailResponse ToOccupationDetailResponse(this Occupation occupation, List<CustomerResponse> customers)
        {
            return new OccupationDetailResponse()
            {
                Id = occupation.Id,
                Name = occupation.Name,
                Descriptions = occupation.Descriptions,
                Status = occupation.Status,
                Customers = customers
            };
        }
    }
}
