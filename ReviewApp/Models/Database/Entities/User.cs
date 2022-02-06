using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database.Entities
{
    public class User{

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long userId { get ; set;}

        [Required]
        public string email{ get; set;}

        [Required]
        public string password{ get; set;}      
        
    }

}