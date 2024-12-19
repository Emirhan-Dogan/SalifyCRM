using SalifyCRM.ActivityManagement.Application.Responses.ActivityStatuses;
using SalifyCRM.ActivityManagement.Application.Responses.ActivityTypes;
using SalifyCRM.ActivityManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Application.Responses.Activities
{
    public class ActivityResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Descriptions { get; set; }
        public int ActivityTypeId { get; set; }
        public int ActivityStatusId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}
