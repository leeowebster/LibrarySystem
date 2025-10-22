using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Models;

namespace LibrarySystem.Application.Interfaces
{
    internal interface IBookService
    {
        void AddBook(string Author, string Title, DateTime DatePublished);

        IEnumerable<Books> GetAllBooks();



    }
}
