using SalifyCRM.ActivityManagement.Application.Responses.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Application.Responses.ActivityNotes
{
    public class ActivityNoteResponse
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string Note { get; set; }
        public DateTime NoteDate { get; set; }
        public string Status { get; set; }

        public ActivityResponse? Activity { get; set; }
    }
}
