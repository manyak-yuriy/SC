using ManagServices.Contracts;
using System.Collections.Generic;
using System;
using DataAccessLayer;

namespace ManagServices.Implementation
{
    public class UserManager : IUserManager
    {
        IUserInfo IUserManager.GetUserInfo(string Name)
        {
            var dbContext = new SofthemeClassroomsDBcontext();
            var users = dbContext.AspNetUsers;
            return null;
        }

        IEnumerable<IUserInfo> IUserManager.GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
