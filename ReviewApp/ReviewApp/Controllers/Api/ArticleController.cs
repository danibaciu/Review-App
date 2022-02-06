using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Request.Article;

namespace ReviewApp.Controllers.Api
{
    [Route("api/[controller]")]
    public class ArticleController : BaseController
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> getArticles()
        {
            return Ok(await _ArticleService.getAllArticles().ConfigureAwait(false));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> getArticleById(long id)
        {
            return Ok(await _ArticleService.getArticlesById(id).ConfigureAwait(false));
        }

        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> getArticleByUserId(long userId)
        {
            return Ok(await _ArticleService.getUserArticles(userId).ConfigureAwait(false));
        }

        [Authorize]
        [HttpGet("tag/{tagId}")]
        public async Task<IActionResult> getArticleByTagId(long tagId)
        {
            return Ok(await _ArticleService.getArticlesByTag(tagId).ConfigureAwait(false));
        }

        [Authorize(Roles = "creator,admin")]
        [HttpPost("insertArticle")]
        public async Task<IActionResult> insertArticles([FromBody] ArticleRequest request)
        {
            return Ok(await _ArticleService.insertNewArticle(request).ConfigureAwait(false));
        }

        [Authorize(Roles = "creator,admin")]
        [HttpPut]
        public async Task<IActionResult> updateArticle([FromBody] ArticleRequest request)
        {
            return Ok(await _ArticleService.updateArticke(request).ConfigureAwait(false));
        }

        [Authorize(Roles = "creator,admin")]
        [HttpPost("deleteArticle")]
        public async Task<IActionResult> deleteArticle([FromBody]ArticleRequest request)
        {
            return Ok(await _ArticleService.deleteArticle(request).ConfigureAwait(false));
        }
    }
}
