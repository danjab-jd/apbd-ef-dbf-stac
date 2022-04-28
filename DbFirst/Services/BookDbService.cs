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
             * await _context.Trips
             * .Include(x => x.ClientTrips).ThenInclude(x => x.IdClientNavigation)
             * 
             */

            /*
             * await _context.ClientTrips
             * .Include(x => x.IdClientNavigation)
             * .Include(x => x.IdTripNavigation)
             * 
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
            // Single - w kolekcji znajduje się MAX. 1 element, który spełnia zapytanie - jeżeli nie ma żadnego - Exception
            // First - w kolekcji znajduje się WIĘCEJ NIŻ 1 element, które spełniają zapytanie.
            //         W wyniku zostanie pobrany pierwszy obiekt "z brzegu" - jeżeli nei ma żadnego - Exception

            // SingleOrDefault/FirstOrDefault - takie samo zachowanie jak przy Single/First,
            //                                  ale gdy nie ma żadnego obiektu spełniającego zapytanie zwróci null
            
            Author authorFromDb = await _context.Authors
                .SingleOrDefaultAsync(x => x.IdAuthor == bookDTO.Author.IdAuthor);

            if(authorFromDb == null)
            {
                // Nie ma autora - dopisanie jakiejś logiki, np. zwracanie odpowiedniego obiektu
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
            Book bookFromDb = await _context.Books
                .SingleOrDefaultAsync(x => x.IdBook == bookDTO.IdBook);

            if(bookFromDb == null)
            {
                // logika gdy ksiazki nie ma w bazie
                return;
            }

            bookFromDb.Title = bookDTO.Title;
            //....

            await _context.SaveChangesAsync();
        }
    }
}
