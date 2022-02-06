using System;
using Models.Database.Entities;

namespace Models.Response.Auth
{
    public class LoginResponseModel
    {
        public long userId { get; set; }
        public Profile profile { get; set; }
        public Preference preference { get; set; }
        public Role role { get; set; }
        public string email { get; set; }
        public string token { get; set; }
    }


}
