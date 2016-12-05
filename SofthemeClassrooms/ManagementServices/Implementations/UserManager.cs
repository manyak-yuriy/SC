using DataAccessLayer;
using ManagementServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Claims;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices.Implementations
{
    public class UsersManager 
    {
        DbRepository db = new DbRepository();
        public void DeleteUser(string userId)
        {
            
        }

        public UserInfo GetUserInfo(string userName)
        {
            throw new NotImplementedException();
        }

        public List<UserInfo> GetUsersInfo()
        {
            var usersInfo = from u in db.Users
                            select u;
            var q = usersInfo.ToList();
            return null;
        }

        public void UpdateUser(UserInfo user)
        {
            throw new NotImplementedException();
        }
    }
}
