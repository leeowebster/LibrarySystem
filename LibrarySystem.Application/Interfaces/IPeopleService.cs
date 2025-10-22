using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain.Interfaces;

namespace LibrarySystem.Application.Interfaces
{
    internal interface IPeopleService
    {
        void RegisterPerson(string Name, string Email);


    }
}
