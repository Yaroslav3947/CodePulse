using CodePulse.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

        // POST: {apibaseurl}/api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request) {
            // Create IdentityUser object

            var user = new IdentityUser {
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim()
            };

            // Create User
            var identityResult = await _userManager.CreateAsync(user, request.Password);

            if(identityResult.Succeeded) {
                // Add Role to user(Reader)
                var identiryResult = await _userManager.AddToRoleAsync(user, "Reader");
                
                if(identiryResult.Succeeded) {
                    return Ok();
                } else {
                    if(identityResult.Errors.Any()) {
                        foreach(var error in identityResult.Errors) {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            } else {
                if(identityResult.Errors.Any()) {
                    foreach (var error in identityResult.Errors) {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return ValidationProblem(ModelState);
        }
    }
}
