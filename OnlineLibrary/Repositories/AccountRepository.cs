using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Interfaces;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetAuthenticatedUserByIdAsync(string identityUserId)
        {
            return await _context.ApplicationUsers
                .Where(user => user.IdentityUser.Id == identityUserId)
                .Include(user => user.IdentityUser).Include(user => user.Purchases)
                .FirstOrDefaultAsync();
        }

        public async Task<Author> GetAuthenticatedAuthorByIdAsync(string identityUserId)
        {
            return await _context.Authors.Where(user => user.IdentityUser.Id == identityUserId)
                .FirstOrDefaultAsync();
        }
    }
}
