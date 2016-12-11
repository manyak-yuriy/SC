using ManagementServices.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PersonalDataViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        public bool Is_Admin { get; set; }

        public int NumberOfEvents { get; set; }

        public static IEnumerable<PersonalDataViewModel> CreateFromUsersInfo(IEnumerable<UserInfo> users)
        {
            List<PersonalDataViewModel> l = new List<PersonalDataViewModel>();
            foreach (var u in users)
            {
                l.Add(CreateFromUserInfo(u));
            }
            return l;
        }

        public static PersonalDataViewModel CreateFromUserInfo(UserInfo user)
        {
            return new PersonalDataViewModel
            {
                Email = user.Email,
                Name = user.FullName,
                Is_Admin = user.Role == "admin",
                NumberOfEvents = user.NumberOfEvents,
                Id = user.UserId
            };
        }
    }
}