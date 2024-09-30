using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IUsersRepository _usersRepository;
        private readonly IProductRepository _productRepository;

        public UsersController(IUsersRepository usersRepository,
            IProductRepository productRepository) {
            this._usersRepository = usersRepository;
            this._productRepository = productRepository;
        }

        // GET: {apibaseurl}/api/users
        [HttpGet]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> GetUsers() {
            var users = await _usersRepository.GetUsersAsync();

            var response = new List<UserDto>();

            foreach(var user in users) {
                response.Add(new UserDto {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    TwoFactorEnabled = user.TwoFactorEnabled
                });
            }

            return Ok(response);
        }

        // GET: {apibaseurl}/api/
        [HttpGet("products/{id}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> GetProductsIDInBasket([FromRoute] Guid id) {
            var response = await _usersRepository.ProductsIDInBasket(id);

            return Ok(response);
        }

        [HttpGet("products/full/{id}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> GetProductsInBasket([FromRoute] Guid id) {
            var productsID = await _usersRepository.ProductsIDInBasket(id);

            var response = new List<ProductDto>();

            foreach(var productID in productsID) {

                var product = await _productRepository.GetByIdAsync(productID);

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


        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id) {
            var existringUser = await _usersRepository.GetUserByIdAsync(id);

            if(existringUser is null) {
                return NotFound(); // 404 
            }

            var response = new UserDto {
                Id = existringUser.Id,
                Email = existringUser.Email,
                PhoneNumber = existringUser.PhoneNumber,
                TwoFactorEnabled = existringUser.TwoFactorEnabled
            };
            return Ok(response); 
        }

        // PUT: {apibaseurl}/api/users{id}
        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditUser([FromRoute] Guid id, UpdateUserRequestDto request) {

            var existingUser = await _usersRepository.GetUserByIdAsync(id);

            // Convert DTO to Domain Model
            var user = new IdentityUser {
                Id = id.ToString(),
                Email = request.Email,
                UserName = request.UserName,
                TwoFactorEnabled = request.TwoFactorEnabled,
                PhoneNumber = request.PhoneNumber,

                NormalizedUserName = existingUser.NormalizedUserName,
                NormalizedEmail = existingUser.NormalizedEmail,
                EmailConfirmed = existingUser.EmailConfirmed,
                PasswordHash = existingUser.PasswordHash,
                SecurityStamp = existingUser.SecurityStamp,
                ConcurrencyStamp = existingUser.ConcurrencyStamp,
                PhoneNumberConfirmed = existingUser.PhoneNumberConfirmed,
                LockoutEnd = existingUser.LockoutEnd,
                LockoutEnabled = existingUser.LockoutEnabled,
                AccessFailedCount = existingUser.AccessFailedCount,
            };

            user = await _usersRepository.UpdateAsync(user);

            if(user is null) {
                return NotFound();
            }

            // Convert Domain model to DTO
            var response = new UserDto {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                TwoFactorEnabled = user.TwoFactorEnabled,
                PhoneNumber = user.PhoneNumber
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id) {
            var user = await _usersRepository.DeleteAsync(id);

            if(user is null) {
                return NotFound();
            }

            // Convert Domain model to Dto
            var response = new UserDto {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                TwoFactorEnabled = user.TwoFactorEnabled
            };

            return Ok(response);
        }
    }
}
