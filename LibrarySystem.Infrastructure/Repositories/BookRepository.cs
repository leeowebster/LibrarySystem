using LibrarySystem.Domain.Models;
using LibrarySystem.Domain.Interfaces;

namespace LibrarySystem.Infrastructure.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly IDbContext _context;

        public BookRepository(IDbContext context)
        {
            _context = context;
        }

        public void Add(Books book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public IEnumerable<Books> GetAll()
        {
            var allBooks = _context.Books.ToList();

            return allBooks;

        }

        public Books GetById(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            return book;
        }

        public Books GetByAuthor(string Author)
        {
            var book = _context.Books.FirstOrDefault(x =>x.Author == Author);
            return book;
        }

        public Books GetByTitle(string Title)
        {
            var book = _context.Books.FirstOrDefault(x=>x.Title == Title);
            return book;
        }

        public void Update(int id)
        {
            //Update later
            var book = GetById(id);
        }

        public List<Books> GetAvailableBooks()
        {
            var book = _context.Books.Where(x => x.IsAvailable == true).ToList();
            return book;
        }
    }
}
