using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database.Entities
{

    public class Article
    {

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Required]
        public string title { get; set; }

        public string content { get; set; }

        public long articleCatId { get; set; }

        public long userId { get; set; }

    }
}