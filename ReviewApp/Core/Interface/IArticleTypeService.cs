using System;
using System.Threading.Tasks;
using Models.Request.Article;
using Models.Response.Generic;

namespace Core.Interface
{
    public interface IArticleTypeService
    {
        Task<CoreResponseModel> insertArticleType(ArticleTypeRequest request);
        Task<CoreResponseModel> getArticleTypes();
        Task<CoreResponseModel> deleteArticleType(long id);
        Task<CoreResponseModel> updateArticleType(ArticleTypeRequest request);
        Task<CoreResponseModel> getArticleTypeById(long id);
    }
}
