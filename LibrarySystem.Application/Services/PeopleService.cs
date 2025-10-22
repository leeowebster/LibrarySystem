using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Models;

namespace LibrarySystem.Application.Services
{
    internal class PeopleService : IPeopleService
    {

        private readonly IBookRepository _bookRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IBorrowRepository _borrowRepository;

        public PeopleService(IBookRepository bookRepository, IPeopleRepository peopleRepository, IBorrowRepository borrowRepository)
        {
            _bookRepository = bookRepository;
            _peopleRepository = peopleRepository;
            _borrowRepository = borrowRepository;
        }

        public void RegisterPerson(string Name, string Email)
        {
            People person = new People()
            {
                Name = Name,
                Email = Email
            };
            _peopleRepository.Add(person);
        }
    }
}
