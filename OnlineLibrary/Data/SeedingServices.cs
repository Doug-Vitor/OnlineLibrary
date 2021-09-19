using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Data
{
    public class SeedingServices
    {
        public static async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Default", "Author" };
            IdentityResult roleResult;

            foreach (string role in roles)
            {
                bool roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public static async Task SeedDb()
        {
        }
    }
}
