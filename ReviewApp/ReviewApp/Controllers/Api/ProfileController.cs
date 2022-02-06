using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Request.Profile;

namespace ReviewApp.Controllers.Api
{
    [Route("api/[controller]")]
    public class ProfileController : BaseController
    {
        [Authorize]
        [HttpPut("updateProfile")]
        public async Task<IActionResult> updateProfile([FromBody] ProfileRequest request)
        {
            return Ok(await _ProfileService.updateProfile(request).ConfigureAwait(false));
        }
    }
}
