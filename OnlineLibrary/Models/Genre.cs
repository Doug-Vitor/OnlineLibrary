using System.Collections.Generic;

namespace OnlineLibrary.Models
{
    public class Genre : Entity
    {
        public string Name { get; set; }
        public virtual List<Book> Books { get; set; }

        public Genre()
        {
        }

        public Genre(string name)
        {
            Name = name;
        }
    }
}
