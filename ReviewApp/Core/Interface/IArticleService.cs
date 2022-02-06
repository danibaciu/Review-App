using System;
using System.Threading.Tasks;
using Models.Request.Article;
using Models.Response.Generic;

namespace Core.Interface
{
    public interface IArticleService
    {
        Task<CoreResponseModel> insertNewArticle(ArticleRequest request);
        Task<CoreResponseModel> getAllArticles();
        Task<CoreResponseModel> getUserArticles(long userId);
        Task<CoreResponseModel> getArticlesByTag(long tagId);
        Task<CoreResponseModel> getArticlesById(long id);
        Task<CoreResponseModel> updateArticke(ArticleRequest request);
        Task<CoreResponseModel> deleteArticle(ArticleRequest request);
    }
}
