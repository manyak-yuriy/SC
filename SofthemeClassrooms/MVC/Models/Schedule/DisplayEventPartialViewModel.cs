using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManagementServices.Models;

namespace WebApplication1.Models.Schedule
{
	public class DisplayEventPartialViewModel
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string OrganizerName { get; set; }
        public int VisitorCount { get; set; }
        public string ApplicationUserId { get; set; }
        public bool AllowSubscription { get; set; }
        public bool IsPublic { get; set; }
        public bool CanEdit { get; set; }

	    public static DisplayEventPartialViewModel ConvertFromEventInfo(EventInfo info)
	    {
	        return  new DisplayEventPartialViewModel()
	        {
	            Title = info.Title,
                AllowSubscription = info.AllowSubscription,
                DateStart = info.DateStart,
                DateEnd = info.DateEnd,
                Description = info.Description,
                OrganizerName = info.OrganizerName,
                ApplicationUserId = info.ApplicationUserID,
                IsPublic = info.IsPublic
	        };
	    }
    }
}