using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.Entities.DTOs;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILibraryAccountManager _accountManager;

        public AccountController( 
            ILibraryAccountManager accountManager)
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

        [Authorize]
        [HttpPost("GetProfile")]
        public async Task<ActionResult> GetProfile()
        {
            var username = User.FindFirst("UserName")?.Value;
            if (username == null)
            {
                return Unauthorized();
            }

            var user = _accountManager.FindUserByUserName(username).Result;
            if (user.Data.UserName == null)
            {
                return NotFound();
            }

            return Ok(user.Data);
        }

        [Authorize]
        [HttpPost("GetLoans")]
        public async Task<ActionResult> GetLoans()
        {
            var username = User.FindFirst("UserName")?.Value;
            if (username == null)
            {
                return Unauthorized();
            }

            var user = _accountManager.FindUserByUserName(username).Result;
            if (user.Data.UserName == null)
            {
                return NotFound();
            }

            var loans = await _accountManager.GetUserLoans(user.Data);
            if (!loans.Success)
            {
                return NotFound(loans.ErrorMessage);
            }

            return Ok(loans.Data);
        }

        [Authorize]
        [HttpPost("ReturnLoans")]
        public async Task<ActionResult> ReturnLoans(int bookId)
        {
            var username = User.FindFirst("UserName")?.Value;
            if (username == null)
            {
                return Unauthorized();
            }

            var user = _accountManager.FindUserByUserName(username).Result;
            if (user.Success == false)
            {
                return NotFound();
            }

            var loans = await _accountManager.ReturnUserLoans(user.Data,bookId);
            if (loans.Success == false)
            {
                return NotFound(loans.ErrorMessage);
            }

            return Ok(loans.Data);
        }

        [Authorize]
        [HttpPost("GetPenalties")]
        public async Task<ActionResult> GetPenalties()
        {
            var username = User.FindFirst("UserName")?.Value;
            if (username == null)
            {
                return Unauthorized();
            }

            var user = _accountManager.FindUserByUserName(username).Result;
            if (!user.Success)
            {
                return NotFound();
            }

            var penalties = await _accountManager.GetUserPenalties(user.Data);
            if (!penalties.Success)
            {
                return NotFound(penalties.ErrorMessage);
            }

            return Ok(penalties.Data);
        }

        [Authorize]
        [HttpPost("PayPenalties")]
        public async Task<ActionResult> PayPenalties(int penltyId, float amount)
        {
            var username = User.FindFirst("UserName")?.Value;
            if (username == null)
            {
                return Unauthorized();
            }

            var user = _accountManager.FindUserByUserName(username).Result;
            if (user.Success == false)
            {
                return NotFound();
            }

            var penalty = await _accountManager.PayUserPenalty(user.Data, penltyId, amount);
            if (penalty.Success == false)
            {
                return NotFound(penalty.ErrorMessage);
            }

            return Ok(penalty.Data);
        }

        [Authorize]
        [HttpPost("RateBook")]
        public async Task<ActionResult> RateBook(int bookId, float rate)
        {
            if (0>rate || rate>5)
            {
                return BadRequest("Puanlandırma 0 ve 5 arasında olmalı.");
            }

            var username = User.FindFirst("UserName")?.Value;
            if (username == null)
            {
                return Unauthorized();
            }

            var user = _accountManager.FindUserByUserName(username).Result;
            if (user.Success == false)
            {
                return NotFound();
            }

            var result = await _accountManager.RateBook(user.Data, bookId, rate);
            if (result.Success == false)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [Authorize]
        [HttpPatch("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(ApplicationUserPatch patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            
            var patchDto = new ApplicationUserPatch();

            if (!TryValidateModel(patchDto))
            {
                return ValidationProblem(ModelState);
            }

            var result = await _accountManager.UpdateeProfileAsync(userId, patchDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
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
