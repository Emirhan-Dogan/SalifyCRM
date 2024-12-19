using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Domain.Entities
{
    public class ActivityNote : IEntity
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string Note { get; set; }
        public DateTime NoteDate { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public int LastUpdatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public Activity? Activity { get; set; }
    }

}
