using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly string _imagesFolderPath;

        public UploadController()
        {
            _imagesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(_imagesFolderPath))
            {
                Directory.CreateDirectory(_imagesFolderPath);
            }
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Görsel dosya yüklenmedi.");
            }

            try
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                var filePath = Path.Combine(_imagesFolderPath, fileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                var relativePath = Path.Combine("Images", fileName);
                return Ok(new { Path = relativePath });
            }
            catch (Exception ex)
            {
                // Hata loglama
                Console.WriteLine($"Hata: {ex.Message}");
                return StatusCode(500, "Dosya yükleme sırasında bir hata oluştu.");
            }
        }
    }
}