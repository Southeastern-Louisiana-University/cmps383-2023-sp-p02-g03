using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Data;
using SP23.P02.Web.Features.Users;
using SP23.P02.Web.Features.Roles;
using Microsoft.AspNetCore.Authorization;

namespace SP23.P02.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public UsersController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }//end UsersController(DataContext)

        //, CreateUserDto createUser
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto user)
        {
            if (user.UserName == null)
            {
                return BadRequest();
            }//end if for UserName == null

            if (!user.Roles.Any())
            {
                return BadRequest("Must have a role");
            }//end if for roles

            //role must exist. return BadRequest if not
            var roleExists = await roleManager.Roles.Select(x => x.Name).ToListAsync();
            foreach (var role in user.Roles)
            {
                if (!roleExists.Contains(role))
                {
                    return BadRequest($"'{role}' does not exist");
                }
            }

            //UserName must not be taken
            var isTaken = await userManager.FindByNameAsync(user.UserName);
            if (isTaken != null)
            {
                return BadRequest("User name is taken");
            }

            //create UserName
            var newUser = new User
            {
                UserName = user.UserName,
            };

            //user must create a password
            /*
            var isPassword = await userManager.CreateAsync(newUser, createUser.Password);
            if (!isPassword.Succeeded)
            {
                return BadRequest("Must have password");
            }
            */

            var assignRole = await userManager.AddToRolesAsync(newUser, user.Roles);
            if (!assignRole.Succeeded)
            {
                return BadRequest();
            }

            return Ok(new UserDto
            {
                Id = newUser.Id,
                Roles = user.Roles,
                UserName = newUser.UserName,
            });
        }//end ActionResult CreateUser

    }//end UsersController

}//end namespace
