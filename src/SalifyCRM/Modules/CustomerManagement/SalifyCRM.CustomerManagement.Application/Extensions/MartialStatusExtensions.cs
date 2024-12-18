using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Application.Responses.MartialStatuses;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class MartialStatusExtensions
    {
        public static MartialStatusResponse ToMartialStatusResponse(this MartialStatus martialStatus)
        {
            return new MartialStatusResponse()
            {
                Id = martialStatus.Id,
                Name = martialStatus.Name,
                Status = martialStatus.Status,
                Descriptions = martialStatus.Descriptions
            };
        }

        public static List<MartialStatusResponse> ToMartialStatusResponseList(this List<MartialStatus> martialStatuses)
        {
            return martialStatuses.Select(
                m => new MartialStatusResponse()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Status = m.Status,
                    Descriptions = m.Descriptions
                }).ToList();
        }

        public static MartialStatusDetailResponse ToMartialStatusDetailResponse(this MartialStatus martialStatus, List<CustomerResponse> customers)
        {
            return new MartialStatusDetailResponse()
            {
                Id = martialStatus.Id,
                Name = martialStatus.Name,
                Status = martialStatus.Status,
                Descriptions = martialStatus.Descriptions,
                Customers = customers
            };
        }
    }
}
