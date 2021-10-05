using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OnlineLibrary.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Data
{
    public class SeedingServices
    {
        private readonly AppDbContext _context;

        public SeedingServices(AppDbContext context)
        {
            _context = context;
        }

        public static async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Default", "Author" };

            foreach (string role in roles)
            {
                bool roleExists = await roleManager.RoleExistsAsync(role);
                if (roleExists is false)
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public void SeedDb()
        {
            if (_context.ApplicationUsers.Any() || _context.Authors.Any() ||_context.Books.Any())
                return;
        }
    }
}
