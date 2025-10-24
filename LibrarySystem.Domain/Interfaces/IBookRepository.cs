namespace LibrarySystem.Domain.Interfaces
{
    public interface IBookRepository
    {
        void Add(Books book);
        Books GetById(int id);
        void Update(int id);
        IEnumerable<Books> GetAll();
        List<Books> GetAvailableBooks();
        public Books GetBookById(int bookId);
    }

    
    }


