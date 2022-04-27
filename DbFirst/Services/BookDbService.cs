using System.Collections.Generic;
using System.Threading.Tasks;
using DbFirst.DTO;

namespace DbFirst.Services
{
    public class BookDbService : IBookDbService
    {
        public Task<IList<BookDTO>> GetBooksListAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task AddBookAsync(BookDTO bookDTO)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateBookAsync(BookDTO bookDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}
