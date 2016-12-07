using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices.Interfaces
{
    interface IFeedback
    {
        void SendMessage(string message, string Name, string LName, string email);
    }
}
