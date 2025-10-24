using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibrarySystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IBorrowService _borrowService;

        public BooksController(IBookService bookService, IBorrowService borrowService)
        {
            _bookService = bookService;
            _borrowService = borrowService;
        }

        // POST api/<BooksController>/AddBook
        [HttpPost("AddBook")]
        public ActionResult AddBook ([FromBody] BookCreate newBook)
        {
            
            if (newBook == null)
                return BadRequest();

            _bookService.AddBook(newBook);
            
            return CreatedAtAction(nameof(GetById), new { id = newBook.Title}, newBook); 
        }


        // GET: api/<BooksController>/AllBooks
        [HttpGet("AllBooks")]
        public IEnumerable<AvailableBooksDTO> Get()
        {
            var AllBooks = _bookService.GetAvailableBooks();
            return AllBooks;
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public ActionResult<BookDTO> GetById(int id)
        {
            return _bookService.GetBookById(id);
        }

        // POST api/Books/Borrow
        [HttpPost("Borrow")]
        public IActionResult Borrow([FromBody] BorrowDTO dto)
        {
            try
            {
                
                _borrowService.Borrow(dto);
                return StatusCode(201);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Return")]
        public IActionResult Return([FromBody] BorrowID borrow )
        {
            try
            {
                _borrowService.ReturnBook(borrow);
                return Ok("Book returned successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }





        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
