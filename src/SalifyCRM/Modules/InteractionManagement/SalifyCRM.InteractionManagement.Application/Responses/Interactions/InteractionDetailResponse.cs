using SalifyCRM.InteractionManagement.Application.Responses.InteractionNotes;
using SalifyCRM.InteractionManagement.Application.Responses.InteractionTypes;
using SalifyCRM.InteractionManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Responses.Interactions
{
    public class InteractionDetailResponse
    {
        public int InteractionTypeId { get; set; }
        public int CustomerId { get; set; }
        public int? OrderId { get; set; }
        public string OrderCode { get; set; }
        public DateTime InteractionDate { get; set; }
        public string Descriptions { get; set; }
        public string Status { get; set; }

        public Customer? Customer { get; set; }
        public InteractionTypeResponse? InteractionType { get; set; }
        public List<InteractionNoteResponse> InteractionNotes { get; set; }
    }
}
