﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Application.Responses.ActivityStatuses
{
    public class ActivityStatusResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }
        public string Status { get; set; }
    }
}
