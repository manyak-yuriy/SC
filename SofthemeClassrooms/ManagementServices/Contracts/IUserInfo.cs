using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagServices.Contracts
{
    interface IUserInfo
    {
        string Id { get; set; }
        string Email { get; set; }
        string FullName { get; set; }
        bool IsAdmin { get; set; }
    }
}
