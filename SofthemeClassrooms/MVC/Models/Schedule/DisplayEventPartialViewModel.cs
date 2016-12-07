using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}