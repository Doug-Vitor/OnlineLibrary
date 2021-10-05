using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Extensions;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Exceptions;
using OnlineLibrary.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class PurchaseRepository : AbstractRepository, IPurchaseRepository
    {
        private readonly HttpContextExtensions _contextExtensions;

        public PurchaseRepository(AppDbContext context, HttpContextExtensions contextExtensions)
            : base(context)
        {
            _contextExtensions = contextExtensions;
        }

        public async Task InsertAsync(Purchase purchase)
        {
            _context.Add(purchase);
            await _context.SaveChangesAsync();
        }

        public async Task<Purchase> GetByIdAsync(int? id)
        {
            if (id is null)
                throw new IdNotProvidedException("ID não informado");

            Purchase purchase = await _context.Purchases.Where(purchase => purchase.Id == id)
                .Include(purchase => purchase.PurchaseDetails).ThenInclude(details => details.Book)
                .ThenInclude(book => book.Author).Include(purchase => purchase.ApplicationUser)
                .ThenInclude(user => user.IdentityUser).FirstOrDefaultAsync();
            if (purchase is null)
                throw new NotFoundException("Não foi possível encontrar uma venda correspondente ao ID fornecido.");

            string userId = _contextExtensions.GetAuthenticatedUserId();
            if (purchase.ApplicationUser.IdentityUser.Id != userId)
                throw new AccessDeniedException("O ID da compra fornecida não pertence a você.");

            return purchase;
        }

        public async Task<IEnumerable<Purchase>> GetByAuthenticatedUserAsync()
        {
            string userId = _contextExtensions.GetAuthenticatedUserId();
            return await _context.Purchases
                .Where(purchase => purchase.ApplicationUser.IdentityUser.Id == userId)
                .Include(purchase => purchase.PurchaseDetails).ThenInclude(details => details.Book)
                .ToListAsync();
        }
    }
}
