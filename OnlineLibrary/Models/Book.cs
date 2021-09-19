namespace OnlineLibrary.Models
{
    public class Book : Entity
    {
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public Book()
        {
        }
    }
}