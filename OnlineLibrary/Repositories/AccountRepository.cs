using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Extensions;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class AccountRepository : AbstractRepository, IAccountRepository
    {
        private readonly HttpContextExtensions _contextExtensions;

        public AccountRepository(AppDbContext context, HttpContextExtensions contextExtensions) 
            : base(context)
        {
            _contextExtensions = contextExtensions;   
        }

        public void DetachEntity(ApplicationUser user)
            => _context.Entry(user).State = EntityState.Detached;

        public async Task<ApplicationUser> GetAuthenticatedUserAsync()
        {
            string authenticatedUserRole = _contextExtensions.GetAuthenticatedUserRole();

            ApplicationUser user = null;
            switch (authenticatedUserRole)
            {
                case "Default":
                    user = await GetAuthenticatedApplicationUserAsync();
                    break;
                case "Author":
                    user = await GetAuthenticatedAuthorAsync();
                    break;
            }

            return user;
        }

        private async Task<ApplicationUser> GetAuthenticatedApplicationUserAsync()
        {
            string userId = _contextExtensions.GetAuthenticatedUserId();
            return await _context.ApplicationUsers
                .Where(user => user.IdentityUser.Id == userId)
                .Include(user => user.IdentityUser).Include(user => user.ShoppingCart)
                .Include(user => user.Purchases).FirstOrDefaultAsync();
        }

        private async Task<Author> GetAuthenticatedAuthorAsync()
        {
            string authenticatedUserId = _contextExtensions.GetAuthenticatedUserId();
            return await _context.Authors.Where(user => user.IdentityUser.Id == authenticatedUserId)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateToAuthorAsync(Author author)
        {
            _context.Update(author);
            await _context.SaveChangesAsync();
            await _context.Database.ExecuteSqlRawAsync($"UPDATE ApplicationUsers SET Discriminator = '{nameof(Author)}' WHERE Id = '{author.Id}'");
        }
    }
}
