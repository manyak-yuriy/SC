using System.Collections.Generic;

namespace ManagServices.Contracts
{
    interface IUserManager
    {
        IUserInfo GetUserInfo(string Name);
        IEnumerable<IUserInfo> GetAllUsers();
    }
}
