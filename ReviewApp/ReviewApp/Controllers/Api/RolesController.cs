using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace ReviewApp.Controllers.Api
{
    [Route("api/[controller]")]
    public class RolesController : BaseController
    {
        [HttpGet("getRoles")]
        public async Task<IActionResult> getRoles()
        {
            return Ok(await _RoleService.getRoles().ConfigureAwait(false));
        }
    }
}
