using ManagementServices.Implementations;
using ManagementServices.Models;
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
        [MaxLength(256, ErrorMessage = "Эмейл не должен быть больше чем 256 символов.")]
        public string Email { get; set; }

        [Required]
        [MaxLength(256, ErrorMessage = "Имя не должно быть больше чем 256 символов.")]
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
                Is_Admin = user.IsAdmin,
                NumberOfEvents = user.NumberOfEvents,
                Id = user.UserId
            };
        }

        public UserInfo ToUserInfo()
        {
            return new UserInfo
            {
                Email = this.Email,
                FullName = this.Name,
                IsAdmin = this.Is_Admin,
                UserId = this.Id
            };
        }
    }
}