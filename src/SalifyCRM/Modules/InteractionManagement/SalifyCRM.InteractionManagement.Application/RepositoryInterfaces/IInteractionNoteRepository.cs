using Core.Persistence;
using SalifyCRM.InteractionManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.RepositoryInterfaces
{
    public interface IInteractionNoteRepository : IEntityBaseRepository<InteractionNote>
    {
    }
}
