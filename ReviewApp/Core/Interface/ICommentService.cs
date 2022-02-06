using System;
using System.Threading.Tasks;
using Models.Request.Comment;
using Models.Response.Generic;

namespace Core.Interface
{
    public interface ICommentService
    {
        Task<CoreResponseModel> insertComment(CommentRequest request);
        Task<CoreResponseModel> getArticleComments(long articleId);
        Task<CoreResponseModel> getUserComments(long userId);
        Task<CoreResponseModel> updateComment(CommentRequest request);
        Task<CoreResponseModel> deleteComment(CommentRequest request);
    }
}
