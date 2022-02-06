
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Request.Auth;

namespace ReviewApp.Controllers.Api
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> register([FromBody] RegisterRequest request)
        {
            return Ok(await _AuthService.register(request).ConfigureAwait(false));
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginRequest request)
        {
            return Ok(await _AuthService.login(request).ConfigureAwait(false));
        }

        [Authorize]
        [HttpPut("updateUser")]
        public async Task<IActionResult> updateUser([FromBody]UpdateUserRequest request)
        {
            return Ok(await _AuthService.updateUser(request).ConfigureAwait(false));
        }
    }
}
