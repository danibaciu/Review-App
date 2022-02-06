using System;
namespace Models.Request.Auth
{
    public class RegisterRequest
    {
        public string email { get; set; }
        public string displayName { get; set; }
        public string password { get; set; }
        public bool gdpr_acceptance { get; set; }
        public bool darkMode { get; set; }
        public int? age { get; set; }
        public long roleId { get; set; }
    }

    public class UpdateUserRequest
    {
        public long userId { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
