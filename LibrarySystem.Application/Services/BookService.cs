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
    internal class BookService : IBookService
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

            var book = new BookCreate(NewBook.Author, NewBook.Title, NewBook.DatePublished);
            

            _bookRepository.Add(book);
        }

        public IEnumerable<Books> GetAllBooks()
        {
            var books = _bookRepository.GetAll();
            return books;
        }



    }
}
