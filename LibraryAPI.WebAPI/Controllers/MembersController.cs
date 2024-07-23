using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Entities.Models;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.Entities.DTOs.MemberDTO;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly ILibraryUserManager<MemberGet, MemberPost, Member> _libraryUserManager;
        private readonly IFileUploadService _fileUploadService;

        public MembersController(ILibraryUserManager<MemberGet, MemberPost, Member> libraryUserManager, IFileUploadService fileUploadService)
        {
            _libraryUserManager = libraryUserManager;
            _fileUploadService = fileUploadService;
        }

        // GET: api/Members
        [Authorize(Roles = "Çalışan,Yönetici")]
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<MemberGet>>> GetAll()
        {
            var result = await _libraryUserManager.GetAllAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [Authorize(Roles = "Yönetici")]
        [HttpGet("GetData")]
        public async Task<ActionResult<IEnumerable<Member>>> GetAllData()
        {
            var result = await _libraryUserManager.GetAllWithDataAsync();

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // GET: api/Members/5
        [Authorize(Roles = "Çalışan,Yönetici")]
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<MemberGet>> Get(string id)
        {
            var result = await _libraryUserManager.GetByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [Authorize(Roles = "Yönetici")]
        [HttpGet("GetData/{id}")]
        public async Task<ActionResult<Member>> GetData(string id)
        {
            var result = await _libraryUserManager.GetWithDataByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Çalışan,Yönetici")]
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(string id, MemberPost member)
        {
            if (member.FileForm != null)
            {
                var imagePath = await _fileUploadService.UploadImage(member.FileForm);
                if (!imagePath.Success)
                {
                    return BadRequest(imagePath.ErrorMessage);
                }
                member.UserImagePath = imagePath.Data;
            }

            var result = await _libraryUserManager.UpdateAsync(id, member);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // POST: api/Members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")]
        [HttpPost("Post")]
        public async Task<ActionResult<MemberPost>> Post(MemberPost member)
        {
            if (member.FileForm != null)
            {
                var imagePath = await _fileUploadService.UploadImage(member.FileForm);
                if (!imagePath.Success)
                {
                    return BadRequest(imagePath.ErrorMessage);
                }
                member.UserImagePath = imagePath.Data;
            }
            else
            {
                member.UserImagePath = null;
            }

            member.UserRoleId = 0;
            var result = await _libraryUserManager.AddAsync(member);
            
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // DELETE: api/Members/5
        [Authorize(Roles = "Çalışan,Yönetici")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _libraryUserManager.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }
    }
}
