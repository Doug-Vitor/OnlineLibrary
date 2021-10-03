using OnlineLibrary.Data;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Interfaces;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class AppUserRepository : AbstractRepository, IAppUserRepository
    {
        public AppUserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task InsertAsync(ApplicationUser user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(ApplicationUser user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
