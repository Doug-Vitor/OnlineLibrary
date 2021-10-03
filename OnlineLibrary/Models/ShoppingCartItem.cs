namespace OnlineLibrary.Models
{
    public class ShoppingCartItem : Entity
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public ShoppingCartItem()
        {
        }

        public ShoppingCartItem(Book book, int quantity)
        {
            Book = book;
            Quantity = quantity;
        }

        public void AddQuantity()
        {
            Quantity++;
        }

        public void DecreaseQuantity()
        {
            Quantity--;
        }
    }
}