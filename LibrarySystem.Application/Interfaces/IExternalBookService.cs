using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Application.DTOs;

namespace LibrarySystem.Application.Interfaces
{
    public interface IExternalBookService
    {
        Task<BookDTO> GetBookDetailsFromExternalAPI(string isbn);

    }
}
