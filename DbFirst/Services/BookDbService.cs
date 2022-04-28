using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbFirst.DTO;
using DbFirst.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbFirst.Services
{
    public class BookDbService : IBookDbService
    {
        private readonly jdContext _context;

        public BookDbService(jdContext context)
        {
            _context = context;
        }

        public async Task<IList<BookDTO>> GetBooksListAsync()
        {
            /*
             * await _context.ClientTrips
             * .Include(x => x.IdClientNavigation)
             * .Include(x => x.IdTripNavigation)
             */

            /*
             * await _context.Books
             * .Include(x => x.IdAuthorNavigation).ThenInclude(x=>x.IdCityDictNav)
             * .Include(x => x.IdPublisherNav)
             */

            return await _context.Books
                .Include(x => x.IdAuthorNavigation)
                .Select(x => new BookDTO
                {
                    IdBook = x.IdBook,
                    Title = x.Title,
                    Author = new()
                    {
                        IdAuthor = x.IdAuthor,
                        FirstName = x.IdAuthorNavigation.Name
                    }
                })
                .ToListAsync();
        }

        public async Task AddBookAsync(BookDTO bookDTO)
        {
            Author authorFromDb = await _context.Authors
                .SingleOrDefaultAsync(x => x.IdAuthor == bookDTO.Author.IdAuthor);

            if(authorFromDb == null)
            {
                // nie znaleziono
                return;
            }

            await _context.Books.AddAsync(new Book
            { 
                Title = bookDTO.Title,
                IdAuthorNavigation = authorFromDb
            });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(BookDTO bookDTO)
        {
            Book bookFromDb = await _context.Books
                .SingleOrDefaultAsync(x => x.IdBook == bookDTO.IdBook);

            if(bookFromDb == null)
            {
                // nie ma ksiazki
                return;
            }

            bookFromDb.Title = bookDTO.Title;
            // zmieniam properties

            await _context.SaveChangesAsync();
        }
    }
}
