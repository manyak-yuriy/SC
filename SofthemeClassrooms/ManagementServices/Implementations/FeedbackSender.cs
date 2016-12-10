using ManagementServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementServices.Models;
using DataAccessLayer;

namespace ManagementServices.Implementations
{
    public class FeedbackSender : IFeedbackSender
    {
        UnitOfWork work = new UnitOfWork();
        public void SendFeedback(FeedBackDTO feedback)
        {
            Feedback f = new Feedback();
            f.Contents = feedback.Message;
            f.Email = feedback.Email;
            f.FirstName = feedback.Name;
            f.LastName = feedback.LastName;
            work.Feedback.Insert(f);
        }
    }
}
