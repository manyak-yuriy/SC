using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Schedule
{
    public class EditEventPartialViewModel
    {
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool AllowSubscription { get; set; }
        public bool IsPublic { get; set; }
        public bool ShowAuthor { get; set; }
        public string OrganizerName { get; set; }
        
        [Required]
        public string RoomId { get; set; }

    }
}