using Microsoft.EntityFrameworkCore.Storage.Json;
using SalifyCRM.InteractionManagement.Application.Responses.InteractionNotes;
using SalifyCRM.InteractionManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Extensions
{
    public static class InteractionNoteExtensions
    {
        public static InteractionNoteResponse ToInteractionNoteResponse(this InteractionNote interactionNote)
        {
            return new InteractionNoteResponse()
            {
                Id = interactionNote.Id,
                Status = interactionNote.Status,
                InteractionId = interactionNote.InteractionId,
                NoteDate = interactionNote.NoteDate,
                Note = interactionNote.Note,
                Interaction = InteractionExtensions.ToInteractionResponse(interactionNote.Interaction)
            };
        }

        public static List<InteractionNoteResponse> ToInteractionNoteResponseList(this List<InteractionNote> interactionNotes)
        {
            return interactionNotes.Select(
                i => new InteractionNoteResponse()
                {
                    Id = i.Id,
                    InteractionId = i.InteractionId,
                    Note = i.Note,
                    NoteDate = i.NoteDate,
                    Status = i.Status,
                    Interaction = InteractionExtensions.ToInteractionResponse(i.Interaction)
                }).ToList();
        }
    }
}
