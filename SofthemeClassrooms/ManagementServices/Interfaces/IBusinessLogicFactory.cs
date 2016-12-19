using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices.Interfaces
{
    public interface IBusinessLogicFactory
    {
        IUserManager UserManager { get; }
        IEventManagment EventManager { get; }
        IFeedbackSender FeedbackSender { get; }
        IEventVisitorActions VisitorsManager { get; }
        IRoomManagment RoomManager { get; }
    }
}
