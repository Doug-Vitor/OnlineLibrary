using System.Collections.Generic;

namespace OnlineLibrary.Models
{
    public class Purchase : Entity
    {
        public virtual List<PurchaseDetails> PurchaseDetails { get; set; }
        public double TotalPrice { get; set; }

        public Purchase()
        {
        }

        public void SetTotalPrice()
        {
            double totalPrice = 0;
            foreach (PurchaseDetails purchaseDetail in PurchaseDetails)
            {
                totalPrice = purchaseDetail.GetTotalPrice();
            }

            TotalPrice = totalPrice;
        }
    }
}
