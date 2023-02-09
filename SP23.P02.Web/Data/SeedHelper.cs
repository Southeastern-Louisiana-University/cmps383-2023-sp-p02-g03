using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.TrainStations;
using SP23.P02.Web.Features.UserRoles;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Data;

public static class SeedHelper
{
    public static async Task MigrateAndSeed(DataContext dataContext, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        await dataContext.Database.MigrateAsync();

        var trainStations = dataContext.Set<TrainStation>();
        var users = dataContext.Set<User>();
        var roles = dataContext.Set<Role>();

        if (!await trainStations.AnyAsync())
        {
            for (int i = 0; i < 3; i++)
            {
                dataContext.Set<TrainStation>()
                    .Add(new TrainStation
                    {
                        Name = "Hammond",
                        Address = "1234 Place st"
                    });
            }

            await dataContext.SaveChangesAsync();
        }

        if(!await roles.AnyAsync())
        {
            await roleManager.CreateAsync(new Role { Name = "Admin" });
            await roleManager.CreateAsync(new Role { Name = "User" });
        }

        if (!await users.AnyAsync())
        {
            var admin = new User { UserName = "galkadi" };
            var user1 = new User { UserName = "bob" };
            var user2 = new User { UserName = "sue" };

            await userManager.CreateAsync(admin, "Password123!");
            await userManager.CreateAsync(user1, "Password123!");
            await userManager.CreateAsync(user2, "Password123!");

            await userManager.AddToRoleAsync(admin, "Admin");
            await userManager.AddToRoleAsync(user1, "User");
            await userManager.AddToRoleAsync(user2, "User");
        }
    }
}