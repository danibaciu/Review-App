using System;
namespace Models.Request.Preference
{
    public class PreferenceRequest
    {
        public long userId { get; set; }
        public bool darkMode { get; set; }
        public bool gdpr_acceptance { get; set; }
    }
}
