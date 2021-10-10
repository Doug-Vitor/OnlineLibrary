using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace OnlineLibrary.Models
{
    public class ShoppingCart : Entity
    {
        public virtual ApplicationUser Buyer { get; set; }
        public int BuyerId { get; set; }
        public virtual List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart()
        {
        }

        public ShoppingCart(ApplicationUser buyer)
        {
            Buyer = buyer;
        }

        public string GetTotalPrice() 
            => ShoppingCartItems.Select(prop => prop.Book.Price * prop.Quantity).Sum()
                .ToString("C2", CultureInfo.CurrentCulture);
    }
}