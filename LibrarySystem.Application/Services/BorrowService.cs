using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Application.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IBorrowRepository _borrowRepository;

        public BorrowService(IBookRepository bookRepository, IPeopleRepository peopleRepository, IBorrowRepository borrowRepository)
        {
            _bookRepository = bookRepository;
            _peopleRepository = peopleRepository;
            _borrowRepository = borrowRepository;
        }

        public void Borrow(BorrowDTO dto)
        {
            var book = _bookRepository.GetBookById(dto.BookId.Value);
            if(book == null)
            {
                throw new InvalidOperationException("Book does not exist.");
            }

            var person = _peopleRepository.GetById(dto.PersonId.Value);
            if(person == null) {
                throw new InvalidOperationException("Person does not exist.");
            }

            if (!book.IsAvailable)
            {
                throw new InvalidOperationException("Book is not available or person does not exist.");
            }
                _borrowRepository.Borrow(book, person);
        }

        public void ReturnBook(BorrowID Borrow)
        {

            var BorrowEntity = _borrowRepository.GetBorrowById(Borrow.Id);
            if (BorrowEntity == null)
            {
                throw new InvalidOperationException("Borrow record does not exist.");
            }
            var BookEntity = _bookRepository.GetBookById(BorrowEntity.BookId);

            if (BorrowEntity.DateReturned is not null)
            {
                throw new InvalidOperationException("This book has already been returned.");
            }
            _borrowRepository.Return(BorrowEntity, BookEntity);

        }

        public int GetBorrowById(BorrowID borrow)
        {
            var borrowRecord = _borrowRepository.GetBorrowById(borrow.Id);
            if (borrowRecord == null)
            {
                throw new InvalidOperationException("Borrow record does not exist.");
            }
            return borrowRecord.Id;
        }

        

    }
}
