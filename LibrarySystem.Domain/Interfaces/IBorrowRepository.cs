

namespace LibrarySystem.Domain.Interfaces
{
    public interface IBorrowRepository
    {
        void Borrow(Books book, People person);
        void Return(Borrow borrow, Books book);

        List<Borrow> DelayedBorrows();

        List<(People person, List<Borrow> Loans)> PeopleAndBooks();

        bool IsBorrowed(int bookid);
        bool IsDelayed(int bookid);

        Borrow GetBorrowByBookId(int bookId);
        Borrow GetBorrowById(int id);

        int GetBorrowCount(int personId);

    }
}
