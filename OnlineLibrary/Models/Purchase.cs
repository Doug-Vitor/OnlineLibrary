using System.Collections.Generic;

namespace OnlineLibrary.Models
{
    public class Purchase : Entity
    {
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual List<PurchaseDetails> PurchaseDetails { get; set; } = new();
        public double TotalPrice { get; set; }

        public Purchase()
        {
        }
    }
}
