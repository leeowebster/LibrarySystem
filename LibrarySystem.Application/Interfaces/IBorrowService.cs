using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Application.DTOs;
using LibrarySystem.Domain.Interfaces;

namespace LibrarySystem.Application.Interfaces
{
    public interface IBorrowService
    {
        
        void Borrow(BorrowDTO dto);

        void ReturnBook(BorrowID Borrow);

    }
}
