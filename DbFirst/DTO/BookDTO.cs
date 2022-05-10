namespace DbFirst.DTO
{
    public class BookDTO
    {
        public int IdBook { get; set; }
        public string Title { get; set; }
        public AuthorDTO Author { get; set; }
    }

    public class AuthorDTO
    {
        public int IdAuthor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
