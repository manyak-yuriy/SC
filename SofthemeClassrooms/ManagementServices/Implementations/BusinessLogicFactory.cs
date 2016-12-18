using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementServices.Interfaces;

namespace ManagementServices.Implementations
{
    public class BusinessLogicFactory : IBusinessLogicFactory
    {
        public IUserManager UserManager
        {
            get
            {
                return new AppUsersManager();
            }
        }

        public IEventActions EventManager
        {
            get
            {
                throw  new NotImplementedException();
            }
        }

        public IFeedbackSender FeedbackSender
        {
            get
            {
                return new FeedbackSender();
            }
        }

        public IEventVisitorActions VisitorsManager
        {
            get
            {
                return new EventVisitorActions();
            }
        }
    }
}
