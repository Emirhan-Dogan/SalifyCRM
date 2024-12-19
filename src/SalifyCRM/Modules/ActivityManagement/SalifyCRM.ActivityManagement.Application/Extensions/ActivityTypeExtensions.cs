using SalifyCRM.ActivityManagement.Application.Responses.ActivityTypes;
using SalifyCRM.ActivityManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Application.Extensions
{
    public static class ActivityTypeExtensions
    {
        public static ActivityTypeResponse ToActivityTypeResponse(this ActivityType activityType)
        {
            return new ActivityTypeResponse()
            {
                Descriptions = activityType.Descriptions,
                Id = activityType.Id,
                Name = activityType.Name,
                Status = activityType.Status
            };
        }

        public static List<ActivityTypeResponse> ToActivityTypeResponseList(this List<ActivityType> activityTypes)
        {
            return activityTypes.Select(
                a=>new ActivityTypeResponse()
                {
                    Descriptions = a.Descriptions,
                    Id = a.Id,
                    Name = a.Name,
                    Status = a.Status
                }).ToList();
        }
    }
}
