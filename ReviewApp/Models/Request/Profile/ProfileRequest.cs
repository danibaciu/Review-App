using System;
namespace Models.Request.Profile
{
    public class ProfileRequest
    {
        public int age { get; set; }
        public string displayName { get; set; }
        public long userId { get; set; }
    }
}
