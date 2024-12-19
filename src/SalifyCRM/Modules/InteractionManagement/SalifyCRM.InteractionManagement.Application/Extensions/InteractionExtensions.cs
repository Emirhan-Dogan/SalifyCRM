using SalifyCRM.InteractionManagement.Application.Responses.Interactions;
using SalifyCRM.InteractionManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Extensions
{
    public static class InteractionExtensions
    {
        public static InteractionResponse ToInteractionResponse(this Interaction interaction)
        {
            return new InteractionResponse()
            {
                CustomerId = interaction.CustomerId,
                Descriptions = interaction.Descriptions,
                InteractionDate = interaction.InteractionDate,
                InteractionTypeId = interaction.InteractionTypeId,
                OrderCode = interaction.OrderCode,
                OrderId = interaction.OrderId,
                Status = interaction.Status
            };
        }

        public static List<InteractionResponse> ToInteractionResponseList(this List<Interaction> interactions)
        {
            return interactions.Select(
                i=> new InteractionResponse()
                {
                    CustomerId = i.CustomerId,
                    Descriptions = i.Descriptions,
                    InteractionDate = i.InteractionDate,
                    InteractionTypeId = i.InteractionTypeId,
                    OrderCode = i.OrderCode,
                    OrderId = i.OrderId,
                    Status = i.Status
                }).ToList();
        }

        public static InteractionDetailResponse ToInteractionDetailResponse(this Interaction interaction, List<InteractionNote> interactionNotes)
        {
            return new InteractionDetailResponse()
            {
                CustomerId = interaction.CustomerId,
                Descriptions = interaction.Descriptions,
                InteractionDate = interaction.InteractionDate,
                InteractionTypeId = interaction.InteractionTypeId,
                OrderCode = interaction.OrderCode,
                OrderId = interaction.OrderId,
                Status = interaction.Status,
                InteractionType = InteractionTypeExtensions.ToInteractionTypeResponse(interaction.InteractionType),
                Customer = interaction.Customer,
                InteractionNotes = InteractionNoteExtensions.ToInteractionNoteResponseList(interactionNotes)
            };
        }
    }
}
