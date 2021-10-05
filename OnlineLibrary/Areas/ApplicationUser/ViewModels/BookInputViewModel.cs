using OnlineLibrary.Models;

namespace OnlineLibrary.Areas.ApplicationUser.ViewModels
{
    public class BookInputViewModel
    {
        public OnlineLibrary.Models.Author Author { get; set; }
        public Book Book { get; set; }

        public BookInputViewModel()
        {
        }

        public BookInputViewModel(OnlineLibrary.Models.Author author)
        {
            Author = author;
        }

        public BookInputViewModel(OnlineLibrary.Models.Author author, Book book)
        {
            Author = author;
            Book = book;
        }
    }
}
