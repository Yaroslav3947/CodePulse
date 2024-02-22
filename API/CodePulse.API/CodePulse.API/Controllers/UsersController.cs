using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository) {
            this._usersRepository = usersRepository;
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
    }
}
