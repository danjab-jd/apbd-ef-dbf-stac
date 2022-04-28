using System;
using System.Collections.Generic;

#nullable disable

namespace DbFirst.Entites
{
    public partial class CityDict
    {
        public CityDict()
        {
            Books = new HashSet<Book>();
        }

        public int IdCityDict { get; set; }
        public string City { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
