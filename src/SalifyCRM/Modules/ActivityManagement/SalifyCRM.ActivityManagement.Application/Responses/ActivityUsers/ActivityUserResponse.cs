using SalifyCRM.ActivityManagement.Application.Responses.Activities;
using SalifyCRM.ActivityManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Application.Responses.ActivityUsers
{
    public class ActivityUserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int ActivityId { get; set; }
        public int UserId { get; set; }
        public bool IsVerifyParticipation { get; set; }
        public DateTime ActivityParticipationDate { get; set; }
        public string Status { get; set; }

        public User? User { get; set; }
        public ActivityResponse? Activity { get; set; }
    }
}
