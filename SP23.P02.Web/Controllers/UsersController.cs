using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Data;
using SP23.P02.Web.Features.Users;
using SP23.P02.Web.Features.Roles;

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

        //will take GET out. avoiding errors for now
        [HttpGet]
        private object GetUserById()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult<UserDto> CreateUser(UserDto user)
        {
            
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }//end ActionResult CreateUser
    }//end UsersController
}//end namespace
