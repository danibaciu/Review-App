using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database.Entities
{
    public class Profile
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        public long userId { get; set; }

        public string displayName { get; set; }

        [DefaultValue(0)]
        public int? age { get; set; }
    }
}
