using System;
namespace Models.Request.Article
{
    public class ArticleRequest
    {
        public long id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public long articleCatId { get; set; }
        public long userId { get; set; }
    }
}
