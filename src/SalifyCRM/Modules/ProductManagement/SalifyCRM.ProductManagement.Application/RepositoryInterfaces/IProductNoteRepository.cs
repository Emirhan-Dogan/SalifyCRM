using Core.Persistence;
using SalifyCRM.ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ProductManagement.Application.RepositoryInterfaces
{
    public interface IProductNoteRepository : IEntityBaseRepository<ProductNote>
    {
    }
}
