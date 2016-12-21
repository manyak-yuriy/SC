using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices.Models
{
    public class EventTableItem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsPublic { get; set; }
        public int ClassroomId { get; set; }
        public string classRoomTitle { get; set; }
    }
}
