using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace OnlineLibrary.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

        public CheckoutViewModel(IEnumerable<ShoppingCartItem> shoppingCartItems)
        {
            ShoppingCartItems = shoppingCartItems;
        }

        public string GetTotalPrice()
        {
            return ShoppingCartItems.Select(item => item.Book.Price * item.Quantity).Sum()
                .ToString("C2", CultureInfo.CurrentCulture);
        }
    }
}
