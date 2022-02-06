using System;
namespace Models.Request.Auth
{
    public class LoginRequest
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
