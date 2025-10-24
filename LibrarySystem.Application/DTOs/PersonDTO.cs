using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; } = 0;
        public string? Name { get; set; }
        public string? Email { get; set; }

    }
}
