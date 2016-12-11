using DataAccessLayer;
using ManagementServices.Interfaces;
using ManagementServices.Models;
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
        UnitOfWork db = new UnitOfWork();
        public void DeleteUser(string userId)
        {
            db.Users.Delete(userId);
        }

        public int GetUserNumber()
        {
            return db.Users.GetAll().Count();
        }

        public UserInfo GetUserInfo(string userName)
        {
            var user = db.Users.GetAll().Where(u => u.UserName == userName).FirstOrDefault();
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
            var users = db.Users.GetAll();
            var usersInfo =
                (from u in users
                 select new UserInfo
                 {
                     FullName = u.Claims.Where(c => ClaimTypes.Name == c.ClaimType).FirstOrDefault().ClaimValue,
                     Email = u.Email,
                     UserId = u.Id,
                     NumberOfEvents = GetNumberUserEvents(u.Id),
                 }).OrderBy(c => c.FullName).Skip(page * itemsPerPage).Take(itemsPerPage);

            return usersInfo;
        }

        public void UpdateUser(UserInfo user)
        {
            var appUser = db.Users.Get(user.UserId);
            if (appUser != null)
            {
                appUser.Email = user.Email;
                appUser.UserName = user.Email;
                appUser.Claims.Where(c => c.ClaimValue == ClaimTypes.Name).First().ClaimValue = user.FullName;

                var role = appUser.Roles.FirstOrDefault();
                var admin = db.Roles.Where(r => r.Name == "admin").FirstOrDefault();
                if (user.Role == "admin" && role.RoleId != admin.Id)
                {
                    role.RoleId = admin.Id;
                }

                db.Users.Update(appUser);
            }
        }

        public string GetUserName(string email)
        {
            var user = db.Users.GetAll().Where(u => u.Email == email)
                .FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            var name = user.Claims.Where(c => c.ClaimType == ClaimTypes.Name)
                .FirstOrDefault().ClaimValue;

            return name;
        }

        public int GetNumberUserEvents(string uId)
        {
            return db.Events.GetAll().Where(e => e.ApplicationUserID == uId).Count();
        }
    }
}
