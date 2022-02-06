using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database.Entities
{

    public class Comment
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        public long user_id { get; set; }

        public long articleId { get; set; }

        public string text { get; set; }
    }
}