using System;
using System.Collections.Generic;

#nullable disable

namespace DbFirst.Entites
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int IdAuthor { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
