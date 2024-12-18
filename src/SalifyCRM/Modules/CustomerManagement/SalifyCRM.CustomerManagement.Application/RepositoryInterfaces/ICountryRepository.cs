using Core.Persistence;
using SalifyCRM.CustomerManagement.Application.Responses.Countries;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.RepositoryInterfaces
{
    public interface ICountryRepository : IEntityBaseRepository<Country>
    {
        void DeleteCountryWithCitiesAndDistricts(int id);
        CountryDetailResponse GetCountryWithCitiesAndDistricts(int id);
        //Task<CountryDetailResponse> GetCountryDetailsByCountryId(int id);
    }
}
