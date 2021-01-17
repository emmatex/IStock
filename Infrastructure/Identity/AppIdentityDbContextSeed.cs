using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            try
            {
                if (!userManager.Users.Any())
                {
                    var user = new AppUser
                    {
                        FullName = "System Admin",
                        Email = "admin@istock.com",
                        UserName = "admin",
                        PhoneNumber = "07065153797",
                    };
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
