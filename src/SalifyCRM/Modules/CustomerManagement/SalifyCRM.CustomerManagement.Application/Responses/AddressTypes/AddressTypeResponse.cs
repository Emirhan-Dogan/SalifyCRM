﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Responses.AddressTypes
{
    public class AddressTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }

        public string Status { get; set; }
    }
}