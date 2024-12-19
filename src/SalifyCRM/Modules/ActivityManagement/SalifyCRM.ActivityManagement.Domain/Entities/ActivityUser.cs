using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Domain.Entities
{
    public class ActivityUser : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int ActivityId { get; set; }
        public int UserId { get; set; }
        public bool IsVerifyParticipation { get; set; }
        public DateTime ActivityParticipationDate { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public int LastUpdatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public User? User { get; set; }
        public Activity? Activity { get; set; }
    }

}
