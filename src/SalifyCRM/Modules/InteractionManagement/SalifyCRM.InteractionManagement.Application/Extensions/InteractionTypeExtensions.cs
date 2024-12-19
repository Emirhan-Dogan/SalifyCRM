using SalifyCRM.InteractionManagement.Application.Responses.InteractionTypes;
using SalifyCRM.InteractionManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Extensions
{
    public static class InteractionTypeExtensions
    {
        public static InteractionTypeResponse ToInteractionTypeResponse(this InteractionType interactionType)
        {
            return new InteractionTypeResponse()
            {
                Descriptions = interactionType.Descriptions,
                Id = interactionType.Id,
                Name = interactionType.Name,
                Status = interactionType.Status
            };
        }

        public static List<InteractionTypeResponse> ToInteractionTypeResponseList(this List<InteractionType> interactionTypes)
        {
            return interactionTypes.Select(
                i => new InteractionTypeResponse()
                {
                    Status = i.Status,
                    Name = i.Name,
                    Id = i.Id,
                    Descriptions = i.Descriptions
                }).ToList();
        }
    }
}
