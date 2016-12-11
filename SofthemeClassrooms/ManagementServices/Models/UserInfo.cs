using ManagementServices.Interfaces;

namespace ManagementServices.Models
{
    public class UserInfo
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public string UserId { get; set; }
        public int NumberOfEvents { get; set; }
    }
}
