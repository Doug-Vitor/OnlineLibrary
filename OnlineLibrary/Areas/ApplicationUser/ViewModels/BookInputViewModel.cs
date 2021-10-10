using OnlineLibrary.Models;
using System.Collections.Generic;

namespace OnlineLibrary.Areas.ApplicationUser.ViewModels
{
    public class BookInputViewModel
    {
        public Author Author { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public Book Book { get; set; }
        
        public BookInputViewModel()
        {
        }

        public BookInputViewModel(Author author, IEnumerable<Genre> genres)
        {
            Author = author;
            Genres = genres;
        }

        public BookInputViewModel(Author author, IEnumerable<Genre> genres, Book book)
        {
            Author = author;
            Genres = genres;
            Book = book;
        }
    }
}
