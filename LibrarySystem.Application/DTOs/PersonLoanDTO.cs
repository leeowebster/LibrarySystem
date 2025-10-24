using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTOs
{
    public class PersonLoanDTO
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int LoanCount { get; set; }


    }
}
