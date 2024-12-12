using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Application.Responses.SocialMedias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Responses.CustomerSocials
{
    public class CustomerSocialResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SocialMediaId { get; set; }

        public string SocialMediaLink { get; set; }
        public string Status { get; set; }

        public CustomerResponse Customer { get; set; }
        public SocialMediaResponse SocialMedia { get; set; }
    }
}
