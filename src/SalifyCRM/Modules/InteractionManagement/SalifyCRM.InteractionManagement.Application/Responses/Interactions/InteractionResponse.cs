using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Responses.Interactions
{
    public class InteractionResponse
    {
        public int InteractionTypeId { get; set; }
        public int CustomerId { get; set; }
        public int? OrderId { get; set; }
        public string OrderCode { get; set; }
        public DateTime InteractionDate { get; set; }
        public string Descriptions { get; set; }
        public string Status { get; set; }
    }
}
