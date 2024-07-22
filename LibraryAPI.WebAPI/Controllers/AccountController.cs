using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryAPI.BLL.Interfaces;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILibraryAccountManager _accountManager;

        public AccountController( ILibraryAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(string userName, string password)
        {
            var user = await _accountManager.FindUserByUserName(userName);
            if (!user.Success)
            {
                return NotFound(user.ErrorMessage);
            }
            var result = await _accountManager.Login(user.Data, password);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("Logout")]
        public async Task<ActionResult> Logout()
        {
            var result = await _accountManager.Logout();
            if (!result.Success)
            {
                return Unauthorized(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpPost("ForgetPassword")]
        public async Task<ActionResult<string>> ForgetPassword(string userName)
        {
            var applicationUser = await _accountManager.FindUserByUserName(userName);
            if (!applicationUser.Success)
            {
                return BadRequest(applicationUser.ErrorMessage);
            }
            var token = await _accountManager.ForgetPassword(applicationUser.Data);
            if (!token.Success)
            {
                return BadRequest(token.ErrorMessage);
            }
            return Ok(token.Data);
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword(string userName, string token, string newPassword)
        {
            var applicationUser = await _accountManager.FindUserByUserName(userName);
            if (!applicationUser.Success)
            {
                return BadRequest(applicationUser.ErrorMessage);
            }
            var result = await _accountManager.ResetPassword(applicationUser.Data, newPassword, token);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [Authorize]
        [HttpPost("RequestEmailChange")]
        public async Task<ActionResult<string>> RequestEmailChange(string userName, string newEmail)
        {
            var applicationUser = await _accountManager.FindUserByUserName(userName);
            if (!applicationUser.Success)
            {
                return BadRequest(applicationUser.ErrorMessage);
            }
            var token = await _accountManager.RequestEmailChange(applicationUser.Data,newEmail);
            if (!token.Success)
            {
                return BadRequest(token.ErrorMessage);
            }
            return Ok(token.Data);
        }

        [Authorize]
        [HttpPost("ChangeEmail")]
        public async Task<ActionResult<string>> ChangeEmail(string userName, string newEmail, string token)
        {
            var applicationUser = await _accountManager.FindUserByUserName(userName);
            if (!applicationUser.Success)
            {
                return BadRequest(applicationUser.ErrorMessage);
            }
            var result = await _accountManager.ChangeEmail(applicationUser.Data, newEmail, token);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [Authorize]
        [HttpPost("RequestEmailConfirm")]
        public async Task<ActionResult<string>> RequestEmailConfirm(string userName)
        {
            var applicationUser = await _accountManager.FindUserByUserName(userName);
            if (!applicationUser.Success)
            {
                return BadRequest(applicationUser.ErrorMessage);
            }
            var token = await _accountManager.RequestEmailConfirm(applicationUser.Data);
            if (!token.Success)
            {
                return BadRequest(token.ErrorMessage);
            }
            return Ok(token.Data);
        }

        [Authorize]
        [HttpPost("ConfirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail(string userName, string token)
        {
            var applicationUser = await _accountManager.FindUserByUserName(userName);
            if (!applicationUser.Success)
            {
                return BadRequest(applicationUser.ErrorMessage);
            }
            var result = await _accountManager.ConfirmEmail(applicationUser.Data, token);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
    }
}
