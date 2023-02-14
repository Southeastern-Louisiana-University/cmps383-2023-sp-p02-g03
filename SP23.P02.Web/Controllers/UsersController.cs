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

            var newUser = new User
            {
                UserName = user.UserName,
            };

            return Ok(new UserDto
            {
                Id = newUser.Id,
                Roles = user.Roles,
                UserName = newUser.UserName,
            }) ;
        }//end ActionResult CreateUser

    }//end UsersController

}//end namespace
