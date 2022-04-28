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
             * await _context.Trips
             * .Include(x => x.CountryTrips).ThenInclude(x => x.Countries)
             * 
             */
            AuthorDTO authorDTO = new AuthorDTO();
            AuthorDTO authorDTO1 = new();

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
                // nie ma autora w bd
                return;
            }

            await _context.Books.AddAsync(new Book
            {
                IdBook = bookDTO.IdBook,
                Title = bookDTO.Title,
                IdAuthorNavigation = authorFromDb
            });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(BookDTO bookDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}
