using System.Collections.Generic;

namespace OnlineLibrary.Models
{
    public class Author : DefaultUser
    {
        public string FullName { get; set; }
        public string ShortBiography { get; set; }

        public virtual List<Book> PublishedBooks { get; set; }

        public Author()
        {
        }
    }
}
