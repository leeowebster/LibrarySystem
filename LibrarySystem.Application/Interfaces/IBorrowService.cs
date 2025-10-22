using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Interfaces;

namespace LibrarySystem.Application.Interfaces
{
    internal interface IBorrowService
    {
        void ReturnBook(
            IBookRepository BookRepo,
            IBorrowRepository BorrowRepo);


    }
}
