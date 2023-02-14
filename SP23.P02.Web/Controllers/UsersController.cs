using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Data;
using SP23.P02.Web.Features.TrainStations;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DbSet<User> users;
        private readonly DataContext dataContext;

        public UsersController(DataContext dataContext)
        {
            this.dataContext = dataContext;
            users = dataContext.Set<User>();
        }//end UsersController(DataContext)

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
