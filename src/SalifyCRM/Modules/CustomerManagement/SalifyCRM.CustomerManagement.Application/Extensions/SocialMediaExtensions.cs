using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Application.Responses.SocialMedias;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class SocialMediaExtensions
    {
        public static SocialMediaResponse ToSocialMediaResponse(this SocialMedia socialMedia)
        {
            return new SocialMediaResponse()
            {
                Id = socialMedia.Id,
                Name = socialMedia.Name,
                Status = socialMedia.Status
            };
        }

        public static List<SocialMediaResponse> ToSocialMediaResponseList(this List<SocialMedia> socialMedias)
        {
            return socialMedias.Select(
                s => new SocialMediaResponse()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Status = s.Status
                }).ToList();
        }

        public static SocialMediaDetailResponse ToSocialMediaDetailResponse(this SocialMedia socialMedia, List<CustomerResponse> customers)
        {
            return new SocialMediaDetailResponse()
            {
                Id = socialMedia.Id,
                Name = socialMedia.Name,
                Icon = socialMedia.Icon,
                Status = socialMedia.Status,
                Customers = customers
            };
        }
    }
}
