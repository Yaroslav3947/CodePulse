using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CodePulse.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository) {
            this._categoryRepository = categoryRepository;
        }
        // 
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryRequestDto request) 
            {
            // Map DTO to Domain Model
            var category = new Category {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await _categoryRepository.CreateAsync(category); // dependency injection

            // Domain to DTO

            var response = new CategoryDto {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        // GET: https://localhost:7105/api/Categories
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCategories() {
            var categories = await _categoryRepository.GetAllAsync();

            // Map Domain model to DTO

            var response = new List<CategoryDto>();
            foreach(var category in categories) {
                response.Add(new CategoryDto {
                     Id = category.Id,
                     Name = category.Name,
                     UrlHandle = category.UrlHandle
                });

            }


            return Ok(response);
        }

        // GET: https://localhost:7105/api/Categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id) {
            var existringCategory = await _categoryRepository.GetById(id);

            if(existringCategory is null) {
                return NotFound(); // 404 
            }

            var response = new CategoryDto {
                Id = existringCategory.Id,
                Name = existringCategory.Name,
                UrlHandle = existringCategory.UrlHandle
            };
            return Ok(response); // 200
        }

        // PUT: https://localhost:7105/api/Categories{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id, UpdateCategoryRequestDto request) {
            
            // Convert DTO to Domain Model
            var category = new Category {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            category = await _categoryRepository.UpdateAsync(category);

            if(category is null) {
                return NotFound();
            }

            // Convert Domain model to DTO

            var response = new CategoryDto {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        // Delete: https://localhost:7105/api/Categories{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id) {
            var category = await _categoryRepository.DeleteAsync(id);

            if(category is null) {
                return NotFound();
            }

            // Convert Domain model to Dto
            var response = new CategoryDto {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }
    }

    

}
