using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Schedule
{
    public class EditEventPartialViewModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool AllowSubscription { get; set; }
        public bool IsPublic { get; set; }
        public bool ShowAuthor { get; set; }
        public string OrganizerName { get; set; }
        
        public string RoomId { get; set; }

    }
}