namespace OnlineLibrary.Models.ViewModels
{
    public class PageDetails
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PreviousPage { get; set; }
        public int NextPage { get; set; }

        public PageDetails()
        {
        }

        public PageDetails(int currentPage, int totalPage)
        {
            TotalPages = totalPage;
            CurrentPage = currentPage;
            PreviousPage = currentPage > 1 ? currentPage - 1 : 1;
            NextPage = CurrentPage < TotalPages ? CurrentPage + 1 : TotalPages;
        }

        public bool HasPreviousPage()
        {
            if (CurrentPage > 1) 
                return true;

            return false;
        }

        public bool HasNextPage()
        {
            if (CurrentPage < TotalPages)
                return true;

            return false;
        }
    }
}