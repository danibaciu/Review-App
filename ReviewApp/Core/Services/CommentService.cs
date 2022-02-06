using System;
using System.Threading.Tasks;
using Core.Interface;
using Microsoft.Extensions.Configuration;
using Models.Database.Context;
using Models.Request.Comment;
using Models.Response.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Core.Services
{
    public class CommentService : ICommentService
    {
        private DatabaseContext _context;
        private IConfiguration _config;
        private ICoreResponseModel _response;
        private ICommonService _common;

        public CommentService(DatabaseContext _context, IConfiguration _config, ICoreResponseModel _response, ICommonService _common)
        {
            this._context = _context;
            this._config = _config;
            this._response = _response;
            this._common = _common;
        }

        public async Task<CoreResponseModel> insertComment(CommentRequest request)
        {
            try
            {
                await _context.comments.AddAsync(new Models.Database.Entities.Comment()
                {
                    text = request.text,
                    user_id = request.userId,
                    articleId = request.articleId
                }).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return _response.getSuccessResponse("Comment inserted successfully", null);
            }
            catch(Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }

        public async Task<CoreResponseModel> getArticleComments(long articleId)
        {
            try
            {
                var comments = await (
                        from comment in _context.comments
                        join users in _context.users
                        on comment.user_id equals users.userId

                        join profile in _context.profiles
                        on users.userId equals profile.userId

                        where (comment.articleId == articleId)
                        select new
                        {
                            id = comment.id,
                            text = comment.text,
                            articleId = comment.articleId,
                            userId = users.userId,
                            displayName = profile.displayName
                        })
                        .OrderByDescending(comment => comment.id)
                        .ToListAsync()
                        .ConfigureAwait(false);

                return _response.getSuccessResponse("Comments Fetched successfully", comments);
            }
            catch (Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }

        public async Task<CoreResponseModel> getUserComments(long userId)
        {
            try
            {
                var comments = await (
                        from comment in _context.comments

                        join articles in _context.articles
                        on comment.articleId equals articles.id

                        join articleType in _context.articleCategories
                        on articles.articleCatId equals articleType.id

                        join users in _context.users
                        on comment.user_id equals users.userId


                        join profile in _context.profiles
                        on users.userId equals profile.userId

                        where (users.userId == userId)
                        select new
                        {
                            id = comment.id,
                            text = comment.text,
                            articleId = comment.articleId,
                            title = articles.title,
                            content = articles.content,
                            articleTagId = articleType.id,
                            articleTagName = articleType.tag,
                            userId = users.userId,
                            displayName = profile.displayName
                        })
                        .ToListAsync()
                        .ConfigureAwait(false);

                return _response.getSuccessResponse("Comments Fetched successfully", comments);
            }
            catch (Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }

        public async Task<CoreResponseModel> updateComment(CommentRequest request)
        {
            try
            {
                var comment = await _context.comments.FirstOrDefaultAsync(x => x.id == request.id).ConfigureAwait(false);
                if (comment == null) return _response.getFailResponse("No comment found", null);

                if (request.userId != comment.user_id)
                {
                    var role = await (
                            from userroles in _context.userRoles
                            join roles in _context.roles
                            on userroles.roleId equals roles.roleId
                            where (userroles.userId == request.userId)
                            select new
                            {
                                roleName = roles.roleName
                            }).SingleOrDefaultAsync().ConfigureAwait(false);
                    if (role.roleName.ToLower() != "admin")
                        return _response.getFailResponse("You are not allowed to edit it", null);
                }
                comment.text = request.text;
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return _response.getSuccessResponse("Comment Updated successfully", comment);
            }
            catch (Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }

        public async Task<CoreResponseModel> deleteComment(CommentRequest request)
        {
            try
            {
                var comment = await _context.comments.FirstOrDefaultAsync(x => x.id == request.id).ConfigureAwait(false);
                if (comment == null) return _response.getFailResponse("No comment found", null);

                if(request.userId != comment.user_id)
                {
                    var role = await (
                            from userroles in _context.userRoles
                            join roles in _context.roles
                            on userroles.roleId equals roles.roleId
                            where (userroles.userId == request.userId)
                            select new
                            {
                                roleName = roles.roleName
                            }).SingleOrDefaultAsync().ConfigureAwait(false);
                    if (role.roleName.ToLower() != "admin")
                        return _response.getFailResponse("You are not allowed to delete it", null);
                }
                _context.Remove(comment);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return _response.getSuccessResponse("Comment Deleted successfully", comment);
            }
            catch (Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }
    }
}
