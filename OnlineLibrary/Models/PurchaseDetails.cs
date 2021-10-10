using System.ComponentModel;

namespace OnlineLibrary.Models
{
    public class PurchaseDetails : Entity
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        [DisplayName("Quantidade")]
        public int Quantity { get; set; }

        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }

        public PurchaseDetails()
        {
        }

        public PurchaseDetails(Book book, int quantity)
        {
            Book = book;
            Quantity = quantity;
        }

        public double GetTotalPrice() => Book.Price * Quantity;
    }
}