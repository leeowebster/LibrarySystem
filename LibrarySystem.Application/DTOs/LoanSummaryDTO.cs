using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTOs
{
    public class LoanSummaryDTO
    {
        public int BorrowId { get; set; }
        public int BookId { get; set; }
        public DateTime DateBorrowed { get; set; }
    }
}
