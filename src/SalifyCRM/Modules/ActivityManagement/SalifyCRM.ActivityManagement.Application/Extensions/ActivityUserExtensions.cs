using SalifyCRM.ActivityManagement.Application.Responses.ActivityUsers;
using SalifyCRM.ActivityManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Application.Extensions
{
    public static class ActivityUserExtensions
    {
        public static ActivityUserResponse ToActivityUserResponse(this ActivityUser activityUser)
        {
            return new ActivityUserResponse()
            {
                Id = activityUser.Id,
                Status = activityUser.Status,
                ActivityId = activityUser.Id,
                ActivityParticipationDate = activityUser.ActivityParticipationDate,
                Email = activityUser.Email,
                IsVerifyParticipation = activityUser.IsVerifyParticipation,
                UserId = activityUser.UserId,
                User = activityUser.User,
                Activity = ActivityExtensions.ToActivityResponse(activityUser.Activity)
            };
        }

        public static List<ActivityUserResponse> ToActivityUserResponseList(this List<ActivityUser> activityUsers)
        {
            return activityUsers.Select(
                u=>new ActivityUserResponse()
                {
                    Id = u.Id,
                    Status = u.Status,
                    ActivityId = u.Id,
                    ActivityParticipationDate = u.ActivityParticipationDate,
                    Email = u.Email,
                    IsVerifyParticipation = u.IsVerifyParticipation,
                    UserId = u.UserId,
                    User = u.User,
                    Activity = ActivityExtensions.ToActivityResponse(u.Activity)
                }).ToList();
        }
    }
}
