using Core.Persistence;
using SalifyCRM.CustomerManagement.Application.Responses.Cities;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.RepositoryInterfaces
{
    public interface ICityRepository : IEntityBaseRepository<City>
    {
        CityDetailResponse GetCityWithDistricts(int id);
    }
}
