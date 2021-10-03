using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class AccountRepository : AbstractRepository, IAccountRepository
    {
        public AccountRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ApplicationUser> GetAuthenticatedUserByIdAsync(string identityUserId)
        {
            return await _context.ApplicationUsers
                .Where(user => user.IdentityUser.Id == identityUserId)
                .Include(user => user.IdentityUser).Include(user => user.ShoppingCart)
                .Include(user => user.Purchases).FirstOrDefaultAsync();
        }

        public async Task<Author> GetAuthenticatedAuthorByIdAsync(string identityUserId)
        {
            return await _context.Authors.Where(user => user.IdentityUser.Id == identityUserId)
                .FirstOrDefaultAsync();
        }
    }
}
