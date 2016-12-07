using DataAccessLayer;
using ManagementServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices.Implementations
{
    class Feedback : IFeedback
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void SendMessage(string message, string Name, string LName, string email)
        {
            
        }
    }
}
