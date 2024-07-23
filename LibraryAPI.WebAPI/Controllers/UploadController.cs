using LibraryAPI.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IFileUploadService _uploadService;

        public UploadController(IFileUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [Authorize(Roles = "Çalışan,Yönetici")]
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile? imageFile)
        {
            var result = await _uploadService.UploadImage(imageFile);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}