using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryApi.DTOs;
using LibraryApi.Data;
using LibraryApi.Models;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }


        //Get request to get all books
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }
        
        //Get request to get a book by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book == null) return NotFound("Book not found");
            return Ok(book);
        }


        //Only Member & Admin can add a book
        [Authorize(Roles = "Member,Admin")]
        [HttpPost("borrow/{id}")]
        public async Task<IActionResult> BorrowBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book == null) return NotFound("Book not found");

            if(book.AvailableCopies <= 0) return BadRequest("No available Copies t borrow");

            book.AvailableCopies -= 1;
            await _context.SaveChangesAsync();
            return Ok(new { message =$"You borrowed {book.Title}. Copies Left: {book.AvailableCopies}"});
        }

        //Only admin can add a book
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                TotalCopies = dto.TotalCopies,
                AvailableCopies = dto.TotalCopies
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new {id = book.Id}, book);
        }


        //Only admin can update a book
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto dto)
        {
            var book = await _context.Books.FindAsync(id);
            if(book == null) return NotFound("Book not found");

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.TotalCopies = dto.TotalCopies;
            book.AvailableCopies = dto.AvailableCopies;

            await _context.SaveChangesAsync();
            return Ok(book);
        }


        //Only admin can delete a book
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Detele(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book == null) return NotFound("Book not found");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
