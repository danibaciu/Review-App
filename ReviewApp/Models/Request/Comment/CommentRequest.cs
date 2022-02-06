using System;
namespace Models.Request.Comment
{
    public class CommentRequest
    {
        public string text { get; set; }
        public long userId { get; set; }
        public long articleId { get; set; }
        public long id { get; set; }

    }
}
