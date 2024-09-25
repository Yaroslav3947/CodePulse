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
    public class ProductsController : ControllerBase {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository) {
            this._productRepository = productRepository;
            this._categoryRepository = categoryRepository;
        }

        // POST: {apibaseurl}/api/product
        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequestDto request) {
            
            // Convert DTO a Domain
            var product = new Product {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
                Description = request.Description,
                UrlHandle = request.UrlHandle,
                FeaturedImageUrl = request.FeaturedImageUrl,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories) {
                var existingCategory = await _categoryRepository.GetById(categoryGuid);
                if(existingCategory is not null) {
                    product.Categories.Add(existingCategory);
                }
            }

            // clarify with Vitya about id, if it is - fucking genius

            product = await _productRepository.CreateAsync(product); // now we have it with id

            // Convert Domain Model back to Dto
            var response = new ProductDto {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock= product.Stock,
                Description = product.Description,
                UrlHandle = product.UrlHandle,
                FeaturedImageUrl = product.FeaturedImageUrl,
                Categories = product.Categories.Select(x => new CategoryDto {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }


        // GET: {apibaseurl}/api/product
        [HttpGet]
        public async Task<IActionResult> GetAllProducts() {
            var products = await _productRepository.GetAllAsync();

            var response = new List<ProductDto>();

            foreach(var product in products) {
                response.Add(new ProductDto {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock,
                    Description = product.Description,
                    UrlHandle = product.UrlHandle,
                    FeaturedImageUrl = product.FeaturedImageUrl,
                    Categories = product.Categories.Select(x => new CategoryDto {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
            }

            return Ok(response);
        }

        // GET: {apiBaseUrl}/api/products/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id) {

            // Get Cosmetic from the repository
            var product = await _productRepository.GetByIdAsync(id);

            if(product is null) {
                return NotFound();
            }

            // Convert Domain model to Dto
            var response = new ProductDto {
                Id = product.Id,
                Name = product.Name,
                Stock= product.Stock,
                Price = product.Price,
                Description = product.Description,
                UrlHandle = product.UrlHandle,
                FeaturedImageUrl = product.FeaturedImageUrl,
                Categories = product.Categories.Select(x => new CategoryDto {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        // PUT: {apiBaseUrl}/api/products/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditProduct([FromRoute] Guid id, UpdateProductRequestDto request) {

            // Convert DTO to Domain Model
            var product = new Product {
                Id = id,
                Name = request.Name,
                Stock = request.Stock,
                Price = request.Price,
                Description = request.Description,
                UrlHandle = request.UrlHandle,
                FeaturedImageUrl = request.FeaturedImageUrl,
                Categories = new List<Category>()
            };

            foreach(var categoryGuid in request.Categories) {
                var existingCategory = await _categoryRepository.GetById(categoryGuid);
                if(existingCategory is not null) {
                    product.Categories.Add(existingCategory);
                }
            }

            product = await _productRepository.UpdateAsync(product);

            if(product is null) {
                return NotFound();
            }

            // Convert Domain model to DTO

            var response = new ProductDto {
                Id = id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                Description = product.Description,
                UrlHandle = product.UrlHandle,
                FeaturedImageUrl = product.FeaturedImageUrl,
                Categories = product.Categories.Select(x => new CategoryDto {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        // Delete: {apiBaseUrl}/api/products/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> ProductCosmetic([FromRoute] Guid id) {
            var product = await _productRepository.DeleteAsync(id);

            if(product is null) {
                return NotFound();
            }

            // Convert Domain model to Dto
            var response = new ProductDto {
                Id = product.Id,
                Name = product.Name,
                Stock= product.Stock,
                Price = product.Price,
                Description = product.Description,
                UrlHandle = product.UrlHandle,
                FeaturedImageUrl = product.FeaturedImageUrl,
                Categories = product.Categories.Select(x => new CategoryDto {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        // Get: {apiBaseUrl}/api/products/{urlHandle}
        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetProductsByUrlHandle([FromRoute] string urlHandle) {
            // Get cosmetic details from the repository

            var product = await _productRepository.GetByUrlHandleAsync(urlHandle);

            if(product is null) {
                return NotFound();
            }

            // Convert Domain model to Dto
            var response = new ProductDto {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                Description = product.Description,
                UrlHandle = product.UrlHandle,
                FeaturedImageUrl = product.FeaturedImageUrl,
                Categories = product.Categories.Select(x => new CategoryDto {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }
    }
}
