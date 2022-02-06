using System;
using System.Threading.Tasks;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Database.Context;
using Models.Database.Entities;
using Models.Request.Article;
using Models.Response.Generic;

namespace Core.Services
{
    public class ArticleTypeService : IArticleTypeService
    {
        private DatabaseContext _context;
        private IConfiguration _config;
        private ICoreResponseModel _response;
        private ICommonService _common;

        public ArticleTypeService(DatabaseContext _context, IConfiguration _config, ICoreResponseModel _response, ICommonService _common)
        {
            this._context = _context;
            this._config = _config;
            this._response = _response;
            this._common = _common;
        }

        public async Task<CoreResponseModel> insertArticleType(ArticleTypeRequest request)
        {
            try
            {
                var articleType = new ArticleCategory()
                {
                    tag = request.tag
                };
                await _context.articleCategories.AddAsync(articleType).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return _response.getSuccessResponse("Inserted Successfully", articleType);
            }
            catch(Exception e)
            {
                return _response.getFailResponse(e.Message, null);
            }
        }

        public async Task<CoreResponseModel> getArticleTypes()
        {
            try
            {
                var collections = await _context.articleCategories.ToListAsync().ConfigureAwait(false);
                return _response.getSuccessResponse("Fetched Successfully", collections);
            }
            catch (Exception e)
            {
                return _response.getFailResponse(e.Message, null);
            }
        }

        public async Task<CoreResponseModel> getArticleTypeById(long id)
        {
            try
            {
                var collection = await _context.articleCategories.FirstOrDefaultAsync(x=>x.id == id).ConfigureAwait(false);
                return _response.getSuccessResponse("Fetched Successfully", collection);
            }
            catch (Exception e)
            {
                return _response.getFailResponse(e.Message, null);
            }
        }

        public async Task<CoreResponseModel> updateArticleType(ArticleTypeRequest request)
        {
            try
            {
                var collection = await _context.articleCategories.FirstOrDefaultAsync(x=>x.id == request.id).ConfigureAwait(false);
                if (collection == null) return _response.getFailResponse("No record found", null);

                collection.tag = request.tag;
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return _response.getSuccessResponse("Updated Successfully", collection);
            }
            catch (Exception e)
            {
                return _response.getFailResponse(e.Message, null);
            }
        }

        public async Task<CoreResponseModel> deleteArticleType(long id)
        {
            try
            {
                var collection = await _context.articleCategories.FirstOrDefaultAsync(x=>x.id == id).ConfigureAwait(false);

                _context.articleCategories.Remove(collection);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return _response.getSuccessResponse("Deleted Successfully", collection);
            }
            catch (Exception e)
            {
                return _response.getFailResponse(e.Message, null);
            }
        }
    }
}
