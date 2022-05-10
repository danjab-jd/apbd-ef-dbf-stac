using System;
using System.Collections.Generic;

#nullable disable

namespace DbFirst.Entities
{
    public partial class Book
    {
        public int IdBook { get; set; }
        public string Title { get; set; }
        public int IdAuthor { get; set; }
        public string PublishYear { get; set; }
        public int IdCityDict { get; set; }

        public virtual Author IdAuthorNavigation { get; set; }
        public virtual CityDict IdCityDictNavigation { get; set; }
    }
}
