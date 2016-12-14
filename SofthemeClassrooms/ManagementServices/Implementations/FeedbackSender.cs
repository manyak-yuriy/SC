using ManagementServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementServices.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;

namespace ManagementServices.Implementations
{
    public class FeedbackSender : IFeedbackSender
    {
        private readonly IDatabaseRepositories _work = new DataBaseRepositories();
        public void SendFeedback(FeedBackDTO feedback)
        {
            Feedback f = new Feedback();
            f.Contents = feedback.Message;
            f.Email = feedback.Email;
            f.FirstName = feedback.Name;
            f.LastName = feedback.LastName;
            _work.Feedback.Insert(f);
        }
    }
}
