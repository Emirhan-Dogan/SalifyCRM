using SalifyCRM.InteractionManagement.Application.Responses.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Responses.InteractionNotes
{
    public class InteractionNoteResponse
    {
        public int Id { get; set; }
        public int InteractionId { get; set; }
        public string Note { get; set; }
        public DateTime NoteDate { get; set; }
        public string Status { get; set; }

        public InteractionResponse? Interaction { get; set; }
    }
}
