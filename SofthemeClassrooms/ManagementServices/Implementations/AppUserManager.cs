using System;
using DataAccessLayer;
using ManagementServices.Interfaces;
using ManagementServices.Models;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Linq;
using DataAccessLayer.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ManagementServices.Implementations
{
    public class AppUsersManager : IUserManager
    {
        private readonly IDatabaseRepositories db = DBFactory.GetDbRepositories();
        private static readonly object _locker = new object();

        public void DeleteUser(string userId)
        {
            lock (_locker)
            {
                db.Users.Delete(userId);
            }
        }

        public int GetUserNumber(string name = null)
        {
            lock (_locker)
            {
                if (name == null)
                {
                    return db.Users.GetAll().Count();
                }

                return db.Users.GetAll().ToList()
                    .Where(u => u.Claims
                                .Where(c => ClaimTypes.Name == c.ClaimValue)
                                .FirstOrDefault().ClaimValue.Contains(name))
                    .Count();
            }
        }

        public UserInfo GetUserInfo(string userName)
        {
            ApplicationUser user;
            string adminId;
            IdentityUserRole role;

            lock (_locker)
            {
                user = db.Users.GetAll()
               .Where(u => u.UserName == userName)
               .First();

                adminId = db.Roles.
                    Where(c => c.Name == "admin").
                    First()
                    .Id;

                role = user.Roles.
                    Where(r => r.RoleId == adminId)
                    .First();
            }

            UserInfo uInfo = new UserInfo
            {
                Email = user.Email,
                UserId = user.Id,
                FullName = user.Claims
                    .Where(c => c.ClaimType == ClaimTypes.Name)
                    .First().ClaimValue,

                NumberOfEvents = GetNumberUserEvents(user.Id),
                IsAdmin = role != null
            };

            return uInfo;
        }

        public IEnumerable<UserInfo> GetUsersInfo(int page, int itemsPerPage, string searchPattern = null)
        {
            lock (_locker)
            {
                IEnumerable<ApplicationUser> users;

                if (searchPattern != null)
                {
                    users = db.Users.GetAll().ToList()
                        .Where(u => u.Claims
                                    .Where(c => ClaimTypes.Name == c.ClaimType)
                                    .FirstOrDefault().ClaimValue.Contains(searchPattern));
                }
                else
                {
                    users = db.Users.GetAll()
                        .OrderBy(c => c.Email)
                        .Skip(page - 1 * itemsPerPage)
                        .Take(itemsPerPage);
                }


                var adminId = db.Roles.
                        Where(c => c.Name == "admin").
                        First()
                        .Id;

                var usersInfo =
                (from u in users.ToList()
                 select new UserInfo
                 {
                     FullName = u.Claims
                         .Where(c => ClaimTypes.Name == c.ClaimType)
                         .First().ClaimValue,
                     Email = u.Email,
                     UserId = u.Id,
                     NumberOfEvents = GetNumberUserEvents(u.Id),
                     IsAdmin = u.Roles.
                                Where(r => r.RoleId == adminId)
                                .FirstOrDefault() != null
                 });


                return usersInfo;
            }
        }

        public void UpdateUser(UserInfo user)
        {
            lock (_locker)
            {
                var appUser = db.Users.Get(user.UserId);
                var roles = db.Roles.ToArray();
                var adminRole = roles.Where(r => r.Name == "admin").First();
                var userRole = roles.Where(r => r.Name == "user").First();

                var personRole = appUser.Roles.FirstOrDefault();

                if (user.IsAdmin &&
                    adminRole.Id != personRole.RoleId)
                {
                    appUser.Roles.Remove(personRole);
                    var newRole = new IdentityUserRole()
                    {
                        RoleId = adminRole.Id,
                        UserId = appUser.Id
                    };

                    appUser.Roles.Add(newRole);
                }
                else if (!user.IsAdmin &&
                         personRole.RoleId != userRole.Id)
                {
                    appUser.Roles.Remove(personRole);
                    var newRole = new IdentityUserRole()
                    {
                        RoleId = userRole.Id,
                        UserId = appUser.Id
                    };

                    appUser.Roles.Add(newRole);
                }

                appUser.Email = user.Email;
                appUser.UserName = user.Email;
                appUser.Claims
                    .Where(c => c.ClaimType == ClaimTypes.Name)
                    .FirstOrDefault()
                    .ClaimValue = user.FullName;

                db.Users.Update(appUser);

            }

        }

        public string GetUserName(string email)
        {
            string name;
            lock (_locker)
            {
                var user = db.Users
                .GetAll()
                .Where(u => u.Email == email)
                .FirstOrDefault();

                if (user == null)
                {
                    return null;
                }

                name = user.Claims
                    .Where(c => c.ClaimType == ClaimTypes.Name)
                    .FirstOrDefault().ClaimValue;
            }

            return name;
        }

        public int GetNumberUserEvents(string uId)
        {
            int count;
            lock (_locker)
            {
                count = db.Events.GetAll()
                .Where(e => e.ApplicationUserID == uId)
                .Count();
            }
            return count;
        }
    }
}

