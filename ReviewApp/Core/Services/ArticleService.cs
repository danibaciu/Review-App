using System;
using System.Threading.Tasks;
using Core.Interface;
using Microsoft.Extensions.Configuration;
using Models.Database.Context;
using Models.Database.Entities;
using Models.Request.Article;
using Models.Response.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class ArticleService : IArticleService
    {
        private DatabaseContext _context;
        private IConfiguration _config;
        private ICoreResponseModel _response;
        private ICommonService _common;

        public ArticleService(DatabaseContext _context, IConfiguration _config, ICoreResponseModel _response, ICommonService _common)
        {
            this._context = _context;
            this._config = _config;
            this._response = _response;
            this._common = _common;
        }

        public async Task<CoreResponseModel> insertNewArticle(ArticleRequest request)
        {
            try
            {
                
                var article = new Article()
                {
                    title = request.title,
                    content = request.content,
                    articleCatId = request.articleCatId,
                    userId = request.userId
                };

                await _context.articles.AddAsync(article).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return _response.getSuccessResponse("Inserted successfully", article);
            }
            catch(Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }

        public async Task<CoreResponseModel> getAllArticles()
        {
            try
            {
                var articles = await (
                        from article in _context.articles

                        join articleType in _context.articleCategories
                        on article.articleCatId equals articleType.id

                        join users in _context.users
                        on article.userId equals users.userId


                        join profile in _context.profiles
                        on users.userId equals profile.userId

                        
                        select new
                        {
                            id = article.id,
                            title = article.title,
                            content = article.content,
                            userId = article.userId,
                            writterName = profile.displayName,
                            articleTag = articleType.tag,
                            articleTagId = articleType.id,
                        })
                        .ToListAsync()
                        .ConfigureAwait(false);



                return _response.getSuccessResponse("Fetched successfully", articles);
            }
            catch (Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }

        public async Task<CoreResponseModel> getUserArticles(long userId)
        {
            try
            {
                var articles = await (
                        from article in _context.articles

                        join articleType in _context.articleCategories
                        on article.articleCatId equals articleType.id

                        join users in _context.users
                        on article.userId equals users.userId

                        join profile in _context.profiles
                        on users.userId equals profile.userId

                        where (article.userId == userId)
                        select new
                        {
                            id = article.id,
                            title = article.title,
                            content = article.content,
                            userId = article.userId,
                            writterName = profile.displayName,
                            articleTag = articleType.tag,
                            articleTagId = articleType.id
                        })
                        .ToListAsync()
                        .ConfigureAwait(false);

                

                return _response.getSuccessResponse("Fetched successfully", articles);
            }
            catch (Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }

        public async Task<CoreResponseModel> getArticlesByTag(long tagId)
        {
            try
            {
                var articles = await (
                        from article in _context.articles

                        join articleType in _context.articleCategories
                        on article.articleCatId equals articleType.id

                        join users in _context.users
                        on article.userId equals users.userId

                        join profile in _context.profiles
                        on users.userId equals profile.userId

                        where (article.articleCatId == tagId)
                        select new
                        {
                            id = article.id,
                            title = article.title,
                            content = article.content,
                            userId = article.userId,
                            writterName = profile.displayName,
                            articleTag = articleType.tag,
                            articleTagId = articleType.id
                        })
                        .ToListAsync()
                        .ConfigureAwait(false);



                return _response.getSuccessResponse("Fetched successfully", articles);
            }
            catch (Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }

        public async Task<CoreResponseModel> getArticlesById(long id)
        {
            try
            {
                var article_ = await (
                        from article in _context.articles

                        join articleType in _context.articleCategories
                        on article.articleCatId equals articleType.id

                        join users in _context.users
                        on article.userId equals users.userId

                        join profile in _context.profiles
                        on users.userId equals profile.userId

                        where (article.id == id)
                        select new
                        {
                            id = article.id,
                            title = article.title,
                            content = article.content,
                            userId = article.userId,
                            writterName = profile.displayName,
                            articleTag = articleType.tag,
                            articleTagId = articleType.id
                        })
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false);



                return _response.getSuccessResponse("Fetched successfully", article_);
            }
            catch (Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }

        public async Task<CoreResponseModel> updateArticke(ArticleRequest request)
        {
            try
            {
                var article = await _context.articles.FirstOrDefaultAsync(x => x.id == request.id).ConfigureAwait(false);
                if (article == null) return _response.getFailResponse("No article found", null);

                if (article.userId != request.userId)
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
                    if(role.roleName.ToLower() != "admin")
                        return _response.getFailResponse("You are not allowed to edit it", null);
                }

                article.articleCatId = request.articleCatId;
                article.content = request.content;
                article.title = request.title;

                await _context.SaveChangesAsync().ConfigureAwait(false);

                return _response.getSuccessResponse("Updated successfully", article);
            }
            catch (Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }

        public async Task<CoreResponseModel> deleteArticle(ArticleRequest request)
        {
            try
            {
                var article = await _context.articles.FirstOrDefaultAsync(x => x.id == request.id).ConfigureAwait(false);
                if (article == null) return _response.getFailResponse("No article found", null);

                if (article.userId != request.userId)
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

                _context.articles.Remove(article);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return _response.getSuccessResponse("Removed successfully", article);
            }
            catch (Exception ex)
            {
                return _response.getFailResponse(ex.Message, null);
            }
        }
    }
}
