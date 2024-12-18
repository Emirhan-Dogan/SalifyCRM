using Core.Persistence;
using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.RepositoryInterfaces
{
    public interface ICustomerRepository : IEntityBaseRepository<Customer>
    {
        List<Customer> GetCustomersBySocialMediaId(int id);
        List<Customer> GetCustomersByCustomerTagId(int id);
        List<Customer> GetCustomersByCityId(int id);
        List<Customer> GetCustomersByCountryId(int id);
        List<Customer> GetCustomersByDistrictId(int id);
    }
}
