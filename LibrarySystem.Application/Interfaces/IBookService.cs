using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Application.DTOs;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Models;

namespace LibrarySystem.Application.Interfaces
{
    public interface IBookService
    {
        void AddBook(BookCreate NewBook);

        public IEnumerable<AvailableBooksDTO> GetAvailableBooks();



    }
}
