using System.Collections.Generic;

namespace OnlineLibrary.Models.ViewModels
{
    public class BookViewModel
    {
        public string CurrentFilter { get; set; }
        public string FilterDetails { get; set; }
        public int? GenreId { get; set; }
        public PageDetails PageDetails { get; set; }
        public IEnumerable<Book> Books { get; set; }

        public BookViewModel()
        {
        }

        public BookViewModel(string currentFilter, int totalPages, int? currentPage, IEnumerable<Book> books)
        {
            CurrentFilter = currentFilter;
            Books = books;
            SetPageDetails(totalPages, currentPage);
        }

        public BookViewModel(string currentFilter, string filterDetails,
            int totalPages,  int? currentPage, IEnumerable<Book> books, int? genreId = null)
        {
            CurrentFilter = currentFilter;
            FilterDetails = filterDetails;
            GenreId = genreId;
            Books = books;
            SetPageDetails(totalPages, currentPage);
        }

        public void SetPageDetails(int totalPage, int? currentPage)
        {
            int currentPageValue = currentPage ?? 1;
            PageDetails = new PageDetails(currentPageValue, totalPage);
        }
    }
}
