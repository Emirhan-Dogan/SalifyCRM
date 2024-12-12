using SalifyCRM.CustomerManagement.Application.Responses.Addresses;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerCategories;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerNotes;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerPermissions;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerSocials;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTags;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTypes;
using SalifyCRM.CustomerManagement.Application.Responses.MartialStatuses;
using SalifyCRM.CustomerManagement.Application.Responses.Occupations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Responses.Customers
{
    public class CustomerDetailResponse
    {
        public int Id { get; set; }
        public int CustomerTypeId { get; set; }
        public int OccupationId { get; set; }
        public int MartialStatusId { get; set; }
        public int CustomerCategoryId { get; set; }

        public string CustomerCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        public int IncomeLevel { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastInteractionDate { get; set; }

        public string Status { get; set; }

        public CustomerTypeResponse CustomerType { get; set; }
        public OccupationResponse Occupation { get; set; }
        public MartialStatusResponse MartialStatus { get; set; }
        public CustomerCategoryResponse CustomerCategory { get; set; }

        public List<CustomerTagResponse> CustomerTags { get; set; }
        public List<AddressResponse> Addresses { get; set; }
        public List<CustomerNoteResponse> CustomerNotes { get; set; }
        public List<CustomerSocialResponse> CustomerSocials { get; set; }
        public List<CustomerPermissionResponse> CustomerPermissions { get; set; }
    }
}
