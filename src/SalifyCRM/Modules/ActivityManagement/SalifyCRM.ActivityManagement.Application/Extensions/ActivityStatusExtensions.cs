using SalifyCRM.ActivityManagement.Application.Responses.ActivityStatuses;
using SalifyCRM.ActivityManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Application.Extensions
{
    public static class ActivityStatusExtensions
    {
        public static ActivityStatusResponse ToActivityStatusResponse(this ActivityStatus activityStatus)
        {
            return new ActivityStatusResponse()
            {
                Descriptions = activityStatus.Descriptions,
                Id = activityStatus.Id,
                Name = activityStatus.Name,
                Status = activityStatus.Status
            };
        }

        public static List<ActivityStatusResponse> ToActivityStatusResponseList(this List<ActivityStatus> activityStatuses)
        {
            return activityStatuses.Select(
                s => new ActivityStatusResponse()
                {
                    Status = s.Status,
                    Id = s.Id,
                    Name = s.Name,
                    Descriptions = s.Descriptions
                }).ToList();
        }
    }
}
