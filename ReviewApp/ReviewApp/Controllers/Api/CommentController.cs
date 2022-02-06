using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Request.Comment;

namespace ReviewApp.Controllers.Api
{
    [Route("api/[controller]")]
    public class CommentController : BaseController
    {
        [Authorize]
        [HttpGet("article/{articleId}")]
        public async Task<IActionResult> getArticleComments(long articleId)
        {
            return Ok(await _CommentService.getArticleComments(articleId).ConfigureAwait(false));
        }

        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> getUserComments(long userId)
        {
            return Ok(await _CommentService.getUserComments(userId).ConfigureAwait(false));
        }

        [Authorize]
        [HttpPost("insertComment")]
        public async Task<IActionResult> insertComment([FromBody] CommentRequest request)
            {
            return Ok(await _CommentService.insertComment(request).ConfigureAwait(false));
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> updateComment([FromBody] CommentRequest request)
        {
            return Ok(await _CommentService.updateComment(request).ConfigureAwait(false));
        }

        [Authorize]
        [HttpPost("deleteComment")]
        public async Task<IActionResult> deleteComment(CommentRequest request)
        {
            return Ok(await _CommentService.deleteComment(request).ConfigureAwait(false));
        }
    }
}
