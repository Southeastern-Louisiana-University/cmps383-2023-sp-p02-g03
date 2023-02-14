using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SP23.P02.Web.Features.Authentication;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AuthenticationController(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = signInManager.UserManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            if(loginDto == null
                || String.IsNullOrWhiteSpace(loginDto.UserName)
                || String.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest();
            }
            
            var signInUser = await userManager.FindByNameAsync(loginDto.UserName);

            if(signInUser == null)
            {
                return BadRequest();
            }

            var signInResult = await signInManager.PasswordSignInAsync(signInUser, loginDto.Password, false, false);

            if(!signInResult.Succeeded)
            {
                return BadRequest();
            }

            var roles = (await userManager.GetRolesAsync(signInUser)).ToArray();
            var userDto = new UserDto
            {
                Id = signInUser.Id,
                UserName = signInUser.UserName,
                Roles = roles,
            };

            return Ok(userDto);
        }

        [HttpGet]
        [Route("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> Me() {
            var user = await userManager.GetUserAsync(signInManager.Context.User);
            if(user == null)
            {
                return BadRequest();
            }

            var roles = (await userManager.GetRolesAsync(user)).ToArray();
            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = roles,
            };

            return Ok(userDto);
        }

        [HttpPost]
        [Route("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
    }
}
