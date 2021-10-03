using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Extensions;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class ShoppingCartRepository : AbstractRepository, IShoppingCartRepository
    {
        private readonly HttpContextExtensions _contextExtensions;

        public ShoppingCartRepository(AppDbContext context, HttpContextExtensions contextExtensions) 
            : base(context)
        {
            _contextExtensions = contextExtensions;
        }

        public async Task InsertAsync(ShoppingCart shoppingCart)
        {
            _context.Add(shoppingCart);
            await _context.SaveChangesAsync();
        }

        public async Task<ShoppingCart> GetByAuthenticatedUserAsync()
        {
            string authenticatedUserId = _contextExtensions.GetAuthenticatedUserId();
            return await _context.ShoppingCarts
                .Where(cart => cart.Buyer.IdentityUser.Id == authenticatedUserId)
                .Include(cart => cart.ShoppingCartItems).ThenInclude(item => item.Book)
                .ThenInclude(book => book.Author).FirstOrDefaultAsync();
        }
    }
}
