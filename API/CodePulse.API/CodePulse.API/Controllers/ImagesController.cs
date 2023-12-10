using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository) {
            this._imageRepository = imageRepository;
        }

        // Post: {apibaseUrl}/api/images
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, 
            [FromForm] string fileName, [FromForm] string title) {

            ValidateFileUpload(file);

            if(ModelState.IsValid) {
                var blogImage = new BlogImage {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now,
                };

                blogImage = await _imageRepository.Upload(file, blogImage);

                // Convrt Domain Model to Dto
                var response = new BlogImageDto {
                    Id = blogImage.Id,
                    FileName = blogImage.FileName,
                    FileExtension = blogImage.FileExtension,
                    Title = blogImage.Title,
                    Url = blogImage.Url,
                    DateCreated = blogImage.DateCreated
                };

                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(IFormFile file) {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower())) {
                ModelState.AddModelError("file", "Unsopported file format.");
            }

            var maxLength = 10485760; // 10mb
            if(file.Length > maxLength) {
                ModelState.AddModelError("file", "File can't be more that 10mb.");
            }
                
        }
    }
}
