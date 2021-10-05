using OnlineLibrary.Models;
using System.Collections.Generic;
using System.Globalization;

namespace OnlineLibrary.Areas.ApplicationUser.ViewModels
{
    public class UserPurchaseViewModel
    {
        public Purchase Purchase { get; set; }
        public IEnumerable<Purchase> Purchases { get; set; }

        public UserPurchaseViewModel(Purchase purchase)
        {
            Purchase = purchase;
        }

        public UserPurchaseViewModel(IEnumerable<Purchase> purchases)
        {
            Purchases = purchases;
        }

        public string GetTotalPrice()
        {
            if (Purchase != null)
                return GetTotalPriceFromSinglePurchase();
            else
                return GetTotalPriceFromPurchases();
        }

        private string GetTotalPriceFromSinglePurchase()
        {
            double totalPrice = 0;
            foreach (PurchaseDetails purchaseDetails in Purchase.PurchaseDetails)
            {
                totalPrice += purchaseDetails.GetTotalPrice();
            }

            return totalPrice.ToString("C2", CultureInfo.CurrentCulture);
        }

        private string GetTotalPriceFromPurchases()
        {
            double totalPrice = 0;
            foreach (Purchase purchase in Purchases)
            {
                foreach (PurchaseDetails purchaseDetails in purchase.PurchaseDetails)
                {
                    totalPrice += purchaseDetails.GetTotalPrice();
                }
            }

            return totalPrice.ToString("C2", CultureInfo.CurrentCulture);
        }
    }
}
