using Azure.Core;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace CodePulse.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CosmeticsController : ControllerBase {
        private readonly ICosmeticRepository _cosmeticRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CosmeticsController(ICosmeticRepository cosmeticRepository, ICategoryRepository categoryRepository) {
            this._cosmeticRepository = cosmeticRepository;
            this._categoryRepository = categoryRepository;
        }

        // POST: {apibaseurl}/api/cosmetics
        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCosmetic([FromBody] CreateCosmeticRequestDto request) {
            
            // Convert DTO a Domain
            var cosmetic = new Cosmetic {
                Name = request.Name,
                Brand = request.Brand,
                Price = request.Price,
                Description = request.Description,
                PublishedDate = request.PublishedDate,
                UrlHandle = request.UrlHandle,
                FeaturedImageUrl = request.FeaturedImageUrl,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories) {
                var existingCategory = await _categoryRepository.GetById(categoryGuid);
                if(existingCategory is not null) {
                    cosmetic.Categories.Add(existingCategory);
                }
            }

            // clarify with Vitya about id, if it is - fucking genius

            cosmetic = await _cosmeticRepository.CreateAsync(cosmetic); // now we have it with id

            // Convert Domain Model back to Dto
            var response = new CosmeticDto {
                Id = cosmetic.Id,
                Name = cosmetic.Name,
                Brand = cosmetic.Brand,
                Price = cosmetic.Price,
                Description = cosmetic.Description,
                PublishedDate = cosmetic.PublishedDate,
                UrlHandle = cosmetic.UrlHandle,
                FeaturedImageUrl = cosmetic.FeaturedImageUrl,
                Categories = cosmetic.Categories.Select(x => new CategoryDto {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }


        // GET: {apibaseurl}/api/cosmetics
        [HttpGet]
        public async Task<IActionResult> GetAllCosmetics() {
            var cosmetics = await _cosmeticRepository.GetAllAsync();

            var response = new List<CosmeticDto>();

            foreach(var cosmetic in cosmetics) {
                response.Add(new CosmeticDto {
                    Id = cosmetic.Id,
                    Name = cosmetic.Name,
                    Brand = cosmetic.Brand,
                    Price = cosmetic.Price,
                    Description = cosmetic.Description,
                    PublishedDate = cosmetic.PublishedDate,
                    UrlHandle = cosmetic.UrlHandle,
                    FeaturedImageUrl = cosmetic.FeaturedImageUrl,
                    Categories = cosmetic.Categories.Select(x => new CategoryDto {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
            }

            return Ok(response);
        }

        // GET: {apiBaseUrl}/api/cosmetics/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCosmeticById([FromRoute] Guid id) {

            // Get Cosmetic from the repository
            var cosmetic = await _cosmeticRepository.GetByIdAsync(id);

            if(cosmetic is null) {
                return NotFound();
            }

            // Convert Domain model to Dto
            var response = new CosmeticDto {
                Id = cosmetic.Id,
                Name = cosmetic.Name,
                Brand = cosmetic.Brand,
                Price = cosmetic.Price,
                Description = cosmetic.Description,
                PublishedDate = cosmetic.PublishedDate,
                UrlHandle = cosmetic.UrlHandle,
                FeaturedImageUrl = cosmetic.FeaturedImageUrl,
                Categories = cosmetic.Categories.Select(x => new CategoryDto {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        // PUT: {apiBaseUrl}/api/cosmetics/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditCosmtetic([FromRoute] Guid id, UpdateCosmeticRequestDto request) {

            // Convert DTO to Domain Model
            var cosmetic = new Cosmetic {
                Id = id,
                Name = request.Name,
                Brand = request.Brand,
                Price = request.Price,
                Description = request.Description,
                PublishedDate = request.PublishedDate,
                UrlHandle = request.UrlHandle,
                FeaturedImageUrl = request.FeaturedImageUrl,
                Categories = new List<Category>()
            };

            foreach(var categoryGuid in request.Categories) {
                var existingCategory = await _categoryRepository.GetById(categoryGuid);
                if(existingCategory is not null) {
                    cosmetic.Categories.Add(existingCategory);
                }
            }

            cosmetic = await _cosmeticRepository.UpdateAsync(cosmetic);

            if(cosmetic is null) {
                return NotFound();
            }

            // Convert Domain model to DTO

            var response = new CosmeticDto {
                Id = id,
                Name = cosmetic.Name,
                Brand = cosmetic.Brand,
                Price = cosmetic.Price,
                Description = cosmetic.Description,
                PublishedDate = cosmetic.PublishedDate,
                UrlHandle = cosmetic.UrlHandle,
                FeaturedImageUrl = cosmetic.FeaturedImageUrl,
                Categories = cosmetic.Categories.Select(x => new CategoryDto {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        // Delete: {apiBaseUrl}/api/blogposts/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteCosmetic([FromRoute] Guid id) {
            var cosmetic = await _cosmeticRepository.DeleteAsync(id);

            if(cosmetic is null) {
                return NotFound();
            }

            // Convert Domain model to Dto
            var response = new CosmeticDto {
                Id = cosmetic.Id,
                Name = cosmetic.Name,
                Brand = cosmetic.Brand,
                Price = cosmetic.Price,
                Description = cosmetic.Description,
                PublishedDate = cosmetic.PublishedDate,
                UrlHandle = cosmetic.UrlHandle,
                FeaturedImageUrl = cosmetic.FeaturedImageUrl,
                Categories = cosmetic.Categories.Select(x => new CategoryDto {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        // Get: {apiBaseUrl}/api/blogposts/{urlHandle}
        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetCosmeticByUrlHandle([FromRoute] string urlHandle) {
            // Get cosmetic details from the repository

            var cosmetic = await _cosmeticRepository.GetByUrlHandleAsync(urlHandle);

            if(cosmetic is null) {
                return NotFound();
            }

            // Convert Domain model to Dto
            var response = new CosmeticDto {
                Id = cosmetic.Id,
                Name = cosmetic.Name,
                Brand = cosmetic.Brand,
                Price = cosmetic.Price,
                Description = cosmetic.Description,
                PublishedDate = cosmetic.PublishedDate,
                UrlHandle = cosmetic.UrlHandle,
                FeaturedImageUrl = cosmetic.FeaturedImageUrl,
                Categories = cosmetic.Categories.Select(x => new CategoryDto {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }
    }
}
