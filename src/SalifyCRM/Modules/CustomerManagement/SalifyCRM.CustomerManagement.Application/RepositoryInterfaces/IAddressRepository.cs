using Core.Persistence;
using Core.Utilities.Results;
using SalifyCRM.CustomerManagement.Application.Responses.Addresses;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.RepositoryInterfaces
{
    public interface IAddressRepository : IEntityBaseRepository<Address>
    {
        List<Address> GetAddressesByCustomerId(int customerId);
    }
}
