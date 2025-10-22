namespace LibrarySystem.Domain.Interfaces
{
    public interface IBookRepository
    {
        void Add(BookCreate bookCreate);
        Books GetById(int id);
        void Update(int id);
        IEnumerable<Books> GetAll();
        List<Books> GetAvailableBooks();
    }

    
    }


