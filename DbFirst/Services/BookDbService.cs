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
            // _context.Trips
            // .Include(x => x.CountryTrips).ThenInclude(x => x.IdCountryNavigation)

            /*
             * _context.ClientTrips
             * .Include(x => x.IdClientNavigation)
             * .Include(x => x.IdTripNavigation)
             */
            return await _context
                .Books
                .Include(x => x.IdAuthorNavigation)
                .Select(x => new BookDTO
                { 
                    IdBook = x.IdBook,
                    Title = x.Title,
                    Author = new()
                    {
                        FirstName = x.IdAuthorNavigation.Name,
                        LastName = x.IdAuthorNavigation.Surname
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
                // Nie ma autora w bazie przerywam żądanie
                return;
            }

            await _context.Books.AddAsync(new Book { 
               
                Title = bookDTO.Title,
                IdAuthorNavigation = authorFromDb
            });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(BookDTO bookDTO)
        {
            Book bookFromDb = await _context.Books.SingleOrDefaultAsync(x => x.IdBook == bookDTO.IdBook);

            if(bookFromDb == null)
            {
                // Brak ksiazki w bazie - przerywamy zadanie
                return;
            }

            bookFromDb.Title = bookDTO.Title;

            await _context.SaveChangesAsync();
        }
    }
}
