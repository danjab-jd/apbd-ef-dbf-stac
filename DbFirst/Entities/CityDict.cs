using System.Collections.Generic;

#nullable disable

namespace DbFirst.Entities
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
