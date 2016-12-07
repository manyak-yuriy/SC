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
    public class AppUsersManager
    {
        DbRepository db = new DbRepository();
        public void DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public UserInfo GetUserInfo(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserInfo> GetUsersInfo()
        {
            var usersInfo = from u in db.Users
                            select new UserInfo
                            {
                                FullName = u.Claims.Where(c => ClaimTypes.Name == c.ClaimType).FirstOrDefault().ClaimValue,
                                Email = u.Email,
                                UserId = u.Id,
                                NumberOfEvents = db.Events.Where(e => e.ApplicationUserID == u.Id).Count()
                            };
       
            return usersInfo;
        }

        public void UpdateUser(UserInfo user)
        {
            throw new NotImplementedException();
        }

        public string GetUserName(string email)
        {
            string name = (from u in db.Users
                           where u.Email == email
                           select u.Claims.Where(c => c.ClaimType == ClaimTypes.Name).FirstOrDefault()).FirstOrDefault().ClaimValue;
            return name;
        }

        public int GetNumberUserEvents(string uId)
        {
            return db.Events.Where(e => e.ApplicationUserID == uId).Count();
        }
    }
}
