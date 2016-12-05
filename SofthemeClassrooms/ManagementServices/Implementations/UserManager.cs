using DataAccessLayer;
using ManagementServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices.Implementations
{
    class UserManager : IUserManager
    {
        DbRepository db = new DbRepository();
        public void DeleteUser(string userId)
        {
           
        }

        public UserInfo GetUserInfo(string userName)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(UserInfo user)
        {
            throw new NotImplementedException();
        }
    }
}
