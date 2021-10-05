using OnlineLibrary.Data;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class AppUserRepository : AbstractRepository, IAppUserRepository
    {
        private readonly IShoppingCartServices _cartServices;

        public AppUserRepository(AppDbContext context, IShoppingCartServices cartServices) : base(context)
        {
            _cartServices = cartServices;
        }

        public async Task InsertAsync(ApplicationUser user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            await _cartServices.CreateCartAsync(user);
        }

        public async Task RemoveAsync(ApplicationUser user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
