using SalifyCRM.CustomerManagement.Application.Responses.CustomerSocials;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Extensions
{
    public static class CustomerSocialExtensions
    {
        public static CustomerSocialResponse ToCustomerSocialResponse(this CustomerSocial customerSocial)
        {
            return new CustomerSocialResponse()
            {
                CustomerId = customerSocial.CustomerId,
                Id = customerSocial.Id,
                Status = customerSocial.Status,
                SocialMediaId = customerSocial.SocialMediaId,
                SocialMediaLink = customerSocial.SocialMediaLink,
                Customer = CustomerExtensions.ToCustomerResponse(customerSocial.Customer),
                SocialMedia = SocialMediaExtensions.ToSocialMediaResponse(customerSocial.SocialMedia)
            };
        }
        public static List<CustomerSocialResponse> ToCustomerSocialResponseList(this List<CustomerSocial> customerSocials)
        {
            return customerSocials.Select(
                c=> new CustomerSocialResponse()
                {
                    CustomerId = c.CustomerId,
                    Id = c.Id,
                    Status = c.Status,
                    SocialMediaId = c.SocialMediaId,
                    SocialMediaLink = c.SocialMediaLink,
                    Customer = CustomerExtensions.ToCustomerResponse(c.Customer),
                    SocialMedia = SocialMediaExtensions.ToSocialMediaResponse(c.SocialMedia)
                }).ToList();
        }
        public static CustomerSocialDetailResponse ToCustomerSocialDetailResponse(this CustomerSocial customerSocial)
        {
            return new CustomerSocialDetailResponse()
            {
                CustomerId = customerSocial.CustomerId,
                Id = customerSocial.Id,
                Status = customerSocial.Status,
                SocialMediaId = customerSocial.SocialMediaId,
                SocialMediaLink = customerSocial.SocialMediaLink,
                Customer = CustomerExtensions.ToCustomerResponse(customerSocial.Customer),
                SocialMedia = SocialMediaExtensions.ToSocialMediaResponse(customerSocial.SocialMedia)
            };
        }
        public static List<CustomerSocialDetailResponse> ToCustomerSocialDetailResponseList(this List<CustomerSocial> customerSocials)
        {
            return customerSocials.Select(
                c => new CustomerSocialDetailResponse()
                {
                    CustomerId = c.CustomerId,
                    Id = c.Id,
                    Status = c.Status,
                    SocialMediaId = c.SocialMediaId,
                    SocialMediaLink = c.SocialMediaLink,
                    Customer = CustomerExtensions.ToCustomerResponse(c.Customer),
                    SocialMedia = SocialMediaExtensions.ToSocialMediaResponse(c.SocialMedia)
                }).ToList();
        }
    }
}
