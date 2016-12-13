using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class UsersPageModel
    {
        public PageInfo PageInfo { get; set; }
        public IEnumerable<PersonalDataViewModel> Users { get; set; }
        public string SearchPattern { get; set; }

        public UsersPageModel()
        {
            PageInfo = new PageInfo();
        }
    }
}