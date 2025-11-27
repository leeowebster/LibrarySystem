using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Models;
using LibrarySystem.Domain.Interfaces;


namespace LibrarySystem.Infrastructure.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly IDbContext _dbContext;
        public BorrowRepository(IDbContext dbcontext) {
            _dbContext = dbcontext;
        }

        public bool IsBorrowed(int bookid)
        {
            var isBorrowed = _dbContext.Borrows.Any(b => b.BookId == bookid && b.DateReturned == null);
            return isBorrowed;
        }

        public bool IsDelayed(int bookid)
        {
            var isDelayed = _dbContext.Borrows.Any(b => b.BookId == bookid && b.DateReturned == null && b.DateBorrowed.AddDays(30) < DateTime.Now);
            return isDelayed;
        }

        public void Borrow(Books book, People person)
        {
                var newBorrow = new Borrow()
                {
                    BookId = book.Id,
                    PeopleID = person.Id,
                    DateBorrowed = DateTime.Now
                };
                _dbContext.Borrows.Add(newBorrow);
                book.IsAvailable = false;
                _dbContext.SaveChanges();
        }

        public void Return(Borrow borrow, Books book)
        {
            borrow.DateReturned = DateTime.Now;
            book.IsAvailable = true;
            _dbContext.SaveChanges();
        }

        public List<(People person, List<Borrow> Loans)> PeopleAndBooks()
        {
            var query = _dbContext.People
            .GroupJoin(
                _dbContext.Borrows.Where(b => b.DateReturned == null),
                person => person.Id,
                borrow => borrow.PeopleID,
                (person, loans) => new ValueTuple<People, List<Borrow>>(person, loans.ToList())
            )
            .ToList();

            return query;
        }

        public List<Borrow> DelayedBorrows()
        {

            var query = _dbContext.Borrows.Where(b => b.DateBorrowed.AddDays(30) < DateTime.Now && b.DateReturned == null).ToList();
            return query;
                
        }
        
       public Borrow GetBorrowByBookId(int bookId)
        {
            return _dbContext.Borrows.FirstOrDefault(b => b.BookId == bookId && b.DateReturned == null);
        }

        public Borrow GetBorrowById(int id)
        {
            return _dbContext.Borrows.FirstOrDefault(b => b.Id == id);
        }

        public int GetBorrowCount(int personId)
        {
            var query = _dbContext.Borrows
                .Where(b => b.PeopleID == personId && b.DateReturned == null)
                .Count();

            return query;
        }
    }
}
