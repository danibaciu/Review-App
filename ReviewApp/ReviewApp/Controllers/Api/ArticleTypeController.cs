using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Request.Article;

namespace ReviewApp.Controllers.Api
{
    [Route("api/[controller]")]
    public class ArticleTypeController : BaseController
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> getTypes()
        {
            return Ok(await _ArticleTypeService.getArticleTypes().ConfigureAwait(false));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> getTypeById(long id)
        {
            return Ok(await _ArticleTypeService.getArticleTypeById(id).ConfigureAwait(false));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> insertNewType([FromBody] ArticleTypeRequest request)
        {
            return Ok(await _ArticleTypeService.insertArticleType(request).ConfigureAwait(false));
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> updateType([FromBody]ArticleTypeRequest request)
        {
            return Ok(await _ArticleTypeService.updateArticleType(request).ConfigureAwait(false));
        }

        [Authorize(Roles ="admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteType(long id)
        {
            return Ok(await _ArticleTypeService.deleteArticleType(id).ConfigureAwait(false));
        }

    }
}
