using System.Collections.Generic;
using System.Threading.Tasks;
using DbFirst.DTO;
using DbFirst.Entites;
using DbFirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace DbFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookDbService _bookDbService;

        public BooksController(IBookDbService bookDbService)
        {
            _bookDbService = bookDbService;
        }

        /*
         * VS i Rider:
         * EntityFrameworkCore
         * EntityFrameworkCore.SqlServer
         * EntityFrameworkCore.Tools
         * 
         * Tylko Rider:
         * EntityFrameworkCore.Tools.DotNet
         * 
         */

        [HttpGet]
        public async Task<IActionResult> GetBooksList()
        {
            IList<BookDTO> result = await _bookDbService.GetBooksListAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO bookDTO)
        {
            await _bookDbService.AddBookAsync(bookDTO);

            return Ok();
        }

        [HttpPut("{idBook}")]
        public async Task<IActionResult> UpdateBook(BookDTO bookDTO)
        {
            await _bookDbService.UpdateBookAsync(bookDTO);

            return Ok();
        }
    }
}
