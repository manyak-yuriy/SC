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
        ApplicationDbContext db = new ApplicationDbContext();
        public void DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public int GetUserNumber()
        {
            return db.Users.Count();
        }

        public  UserInfo GetUserInfo(string userName)
        {
            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            var adminId = db.Roles.Where(c => c.Name == "admin").FirstOrDefault().Id;
            var role = user.Roles.Where(r => r.RoleId == adminId).FirstOrDefault();
            UserInfo uInfo = new UserInfo
            {
                Email = user.Email,
                UserId = user.Id,
                FullName = user.Claims.Where(c => c.ClaimType == ClaimTypes.Name).FirstOrDefault().ClaimValue,
                NumberOfEvents = GetNumberUserEvents(user.Id),
                Role = role == null ? "user" : "admin"
            };

            return uInfo;
        }

        public IEnumerable<UserInfo> GetUsersInfo(int page, int itemsPerPage)
        {
            var usersInfo = (from u in db.Users
                            select new UserInfo
                            {
                                FullName = u.Claims.Where(c => ClaimTypes.Name == c.ClaimType).FirstOrDefault().ClaimValue,
                                Email = u.Email,
                                UserId = u.Id,
                                NumberOfEvents = db.Event.Where(e => e.ApplicationUserID == u.Id).Count()
                            }).OrderBy(c => c.FullName).Skip(page*itemsPerPage).Take(itemsPerPage);
       
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
            return db.Event.Where(e => e.ApplicationUserID == uId).Count();
        }
    }
}
