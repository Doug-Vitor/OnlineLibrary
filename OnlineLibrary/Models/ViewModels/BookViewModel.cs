using System.Collections.Generic;
using System.IO;

namespace OnlineLibrary.Models.ViewModels
{
    public class BookViewModel
    {
        public string CurrentFilter { get; set; }
        public string FilterDetails { get; set; }
        public int? Page { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<Book> Books { get; set; }

        public BookViewModel()
        {
        }

        public BookViewModel(string currentFilter, int? page, int pageCount,
            IEnumerable<Book> books)
        {
            CurrentFilter = currentFilter;
            Page = page;
            PageCount = pageCount;
            Books = books;
        }

        public BookViewModel(string currentFilter, string filterDetails, int page, 
            int pageCount, IEnumerable<Book> books)
        {
            CurrentFilter = currentFilter;
            FilterDetails = filterDetails;
            Page = page;
            PageCount = pageCount;
            Books = books;
        }
    }
}
