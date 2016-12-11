using ManagementServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices.Interfaces
{
    interface IFeedbackSender
    {
        void SendFeedback(FeedBackDTO feedback);
    }
}
