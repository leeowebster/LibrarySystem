using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Models;

namespace LibrarySystem.Application.Services
{
    public class PeopleService : IPeopleService
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

        public void RegisterPerson(PersonDTO NewPerson)
        {
            People person = new()
            {
                Name = NewPerson.Name,
                Email = NewPerson.Email
            };
            if(ValidateDuplicateEmail(person.Email) == true)
            {
                throw new Exception("Email already registered.");
            }

            _peopleRepository.Add(person);
        }


        internal bool ValidateDuplicateEmail(string email)
        {
            var allPeople = _peopleRepository.GetAll();
            return allPeople.Any(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }


    }
}
