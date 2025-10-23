using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Models;

namespace LibrarySystem.Application.Services
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _bookRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IBorrowRepository _borrowRepository;

        public BookService(IBookRepository bookRepository, IPeopleRepository peopleRepository, IBorrowRepository borrowRepository)
        {
            _bookRepository = bookRepository;
            _peopleRepository = peopleRepository;
            _borrowRepository = borrowRepository;
        }

        public void AddBook(BookCreate NewBook)
        {

            var book = new Books()
            {
                Author = NewBook.Author,
                Title = NewBook.Title,
                DatePublished = NewBook.DatePublished,
                IsAvailable = true
            };

            _bookRepository.Add(book);
        }

        public IEnumerable<AvailableBooksDTO> GetAvailableBooks()
        {
            var AvailableBooks = _bookRepository.GetAvailableBooks();
            
            var BookDTO = AvailableBooks.Select(b => new AvailableBooksDTO
            {
                Id = b.Id,
                Author = b.Author,
                Title = b.Title
            });

            return BookDTO;
        }



    }
}
