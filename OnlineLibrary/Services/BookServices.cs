using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLibrary.Services
{
    public class BookServices : IBookServices
    {
        private readonly IBookRepository _bookRepository;
        private readonly IShoppingCartItemsRepository _cartItemsRepository;
        private readonly IPurchaseRepository _purchaseRepository;

        public BookServices(IBookRepository bookRepository, IShoppingCartItemsRepository cartItemsRepository,
            IPurchaseRepository purchaseRepository)
        {
            _bookRepository = bookRepository;
            _cartItemsRepository = cartItemsRepository;
            _purchaseRepository = purchaseRepository;
        }

        public async Task<IEnumerable<Book>> ReturnBookListByUserPurchasesAsync()
        {
            IEnumerable<Purchase> purchases = await _purchaseRepository.GetByAuthenticatedUserAsync();
            List<Book> books = new();

            foreach (Purchase purchase in purchases)
            {
                foreach (PurchaseDetails purchaseDetails in purchase.PurchaseDetails)
                {
                    books.Add(await _bookRepository.GetByIdAsync(purchaseDetails.BookId));
                }
            }

            return books;
        }
    }
}
