using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Request.Preference;

namespace ReviewApp.Controllers.Api
{
    [Route("api/[controller]")]
    public class PreferenceController : BaseController
    {
        [Authorize]
        [HttpPut("updatePreference")]
        public async Task<IActionResult> updatePreference([FromBody] PreferenceRequest request)
        {
            return Ok(await _PreferenceService.updatePreference(request).ConfigureAwait(false));
        }
    }
}
