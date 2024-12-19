using SalifyCRM.ActivityManagement.Application.Responses.Activities;
using SalifyCRM.ActivityManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Application.Extensions
{
    public static class ActivityExtensions
    {
        public static ActivityResponse ToActivityResponse(this Activity activity)
        {
            return new ActivityResponse()
            {
                Status = activity.Status,
                ActivityStatusId = activity.ActivityStatusId,
                ActivityTypeId = activity.ActivityTypeId,
                CustomerId = activity.CustomerId,
                Descriptions = activity.Descriptions,
                EndDate = activity.EndDate,
                Id = activity.Id,
                Location = activity.Location,
                Name = activity.Name,
                StartDate = activity.StartDate
            };
        }

        public static List<ActivityResponse> ToActivityResponseList(this List<Activity> activities)
        {
            return activities.Select(
                a => new ActivityResponse()
                {
                    Status = a.Status,
                    ActivityStatusId = a.ActivityStatusId,
                    ActivityTypeId = a.ActivityTypeId,
                    CustomerId = a.CustomerId,
                    Descriptions = a.Descriptions,
                    EndDate = a.EndDate,
                    Id = a.Id,
                    Location = a.Location,
                    Name = a.Name,
                    StartDate = a.StartDate
                }).ToList();
        }

        public static ActivityDetailResponse ToActivityDetailResponse(this Activity activity, List<ActivityNote> activityNotes)
        {
            return new ActivityDetailResponse()
            {
                Status = activity.Status,
                ActivityStatusId = activity.ActivityStatusId,
                ActivityTypeId = activity.ActivityTypeId,
                CustomerId = activity.CustomerId,
                Id = activity.Id,
                Location = activity.Location,
                Name = activity.Name,
                Descriptions = activity.Descriptions,
                EndDate = activity.EndDate,
                StartDate = activity.StartDate,
                ActivityStatus = ActivityStatusExtensions.ToActivityStatusResponse(activity.ActivityStatus),
                ActivityType = ActivityTypeExtensions.ToActivityTypeResponse(activity.ActivityType),
                Customer = activity.Customer,
                ActivityNotes = ActivityNoteExtensions.ToActivityNoteResponseList(activityNotes)
            };
        }
    }
}
