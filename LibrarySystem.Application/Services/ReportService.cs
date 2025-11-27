using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IBorrowRepository _borrowRepository;
        public ReportService(IBorrowRepository borrowRepository)
        {
            _borrowRepository = borrowRepository;
        }

        public IEnumerable<(People, List<Borrow>)> ActiveBorrows()
        {
            var domainData = _borrowRepository.PeopleAndBooks();

            //var resultDTO = domainData.Where(b => b.Loans.Any()).
            //    Select(pd => new PersonLoanDTO
            //    {
            //        PersonId = pd.person.Id,
            //        Name = pd.person.Name,
            //        Email = pd.person.Email,
            //        LoanCount = pd.Loans.Count()
            //    }

            //    ).ToList();
            return domainData;
        }

        

        public IEnumerable<BorrowDTO> DelayedBorrows()
        {
            var domainData = _borrowRepository.DelayedBorrows();
            var resultDTO = domainData.Select(b => new BorrowDTO
            {
                Id = b.Id,
                BookId = b.BookId,
                PersonId = b.PeopleID
            }).ToList();
            return resultDTO;
        }
    }
}
