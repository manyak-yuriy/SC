using ManagementServices.Interfaces;

namespace ManagementServices.Implementations
{
    public class UserInfo
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Is_Admin { get; set; }

        public int NumberOfEvents { get; set; }
    }
}
