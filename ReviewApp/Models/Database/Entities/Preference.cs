using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database.Entities
{
    public class Preference
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        public long userId { get; set; }

        [DefaultValue(false)]
        public bool darkMode { get; set; }

        public bool gdpr_acceptance { get; set; }
    }
}
