using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Application.DTOs;
using LibrarySystem.Domain.Models;

namespace LibrarySystem.Application.Interfaces
{
    public interface IReportService
    {
        IEnumerable<PersonLoanDTO> ActiveBorrows();
        IEnumerable<BorrowDTO> DelayedBorrows();

    }
}
