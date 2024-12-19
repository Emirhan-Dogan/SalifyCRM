using Microsoft.EntityFrameworkCore.Storage.Json;
using SalifyCRM.ActivityManagement.Application.Responses.ActivityNotes;
using SalifyCRM.ActivityManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Application.Extensions
{
    public static class ActivityNoteExtensions
    {
        public static ActivityNoteResponse ToActivityNoteResponse(this ActivityNote activityNote)
        {
            return new ActivityNoteResponse()
            {
                ActivityId = activityNote.ActivityId,
                Id = activityNote.Id,
                NoteDate = activityNote.NoteDate,
                Note = activityNote.Note,
                Status = activityNote.Status,
                Activity = ActivityExtensions.ToActivityResponse(activityNote.Activity)
            };
        }

        public static List<ActivityNoteResponse> ToActivityNoteResponseList(this List<ActivityNote> activityNotes)
        {
            return activityNotes.Select(
                n=>new ActivityNoteResponse()
                {
                    ActivityId = n.ActivityId,
                    Id = n.Id,
                    NoteDate = n.NoteDate,
                    Note = n.Note,
                    Status = n.Status,
                    Activity = ActivityExtensions.ToActivityResponse(n.Activity)
                }).ToList();
        }
    }
}
