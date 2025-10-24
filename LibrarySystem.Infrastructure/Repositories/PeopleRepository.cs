using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Models;
using LibrarySystem.Domain.Interfaces;

namespace LibrarySystem.Infrastructure.Repositories
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly IDbContext _context;

        public PeopleRepository(IDbContext context)
        {
            _context = context;
        }

        public void Add(People person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
        }

        public IEnumerable<People> GetAll()
        {
            var returnAll = _context.People.ToList();
            return returnAll;
        }

        public People GetById(int id)
        {
            var person = _context.People.FirstOrDefault(x => x.Id == id);
            return person;
        }

        

        public void Update(People people)
        {
            throw new NotImplementedException();
        }
    }
}
