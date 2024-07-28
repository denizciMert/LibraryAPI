using LibraryAPI.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class BanController : ControllerBase
    {
        private readonly ILibraryAccountManager _accountManager;

        public BanController(
            ILibraryAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost("Ban")]
        public async Task<ActionResult> Ban(string userName)
        {
            var user = await _accountManager.FindUserByUserName(userName);
            if (!user.Success)
            {
                return NotFound(user.ErrorMessage);
            }

            var result = await _accountManager.BanUser(user.Data);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpPost("UnBan")]
        public async Task<ActionResult> UnBan(string userName)
        {
            var user = await _accountManager.FindUserByUserName(userName);
            if (!user.Success)
            {
                return NotFound(user.ErrorMessage);
            }
            var result = await _accountManager.UnBanUser(user.Data);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
    }
}
