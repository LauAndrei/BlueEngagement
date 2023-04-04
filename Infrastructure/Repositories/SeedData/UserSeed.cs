using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories.SeedData;

public static class UserSeed
{
    public static async Task SeedUsersAsync(UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new User()
            {
                FirstName = "Test1",
                LastName = "Employee",
                Email = "testemployee@test.com",
                UserName = "FirstEmployee",
                
            };
            
            await userManager.CreateAsync(user, "Password1!");
            await userManager.AddToRoleAsync(user, ROLES_CONSTANTS.ROLES.EMPLOYEE);
            
            var user2 = new User()
            {
                FirstName = "Test2",
                LastName = "Manager",
                Email = "testmanager@test.com",
                UserName = "FirstManager",
                
            };

            await userManager.CreateAsync(user2, "Password1!");
            await userManager.AddToRoleAsync(user2, ROLES_CONSTANTS.ROLES.MANAGER);
        }
    }
}