using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTOs
{
    public class BookDTO
    {
        public int BookId { get; set; } = 0;
        public string Title { get; set; } = null;
        public string Author { get; set; } = null;

    }
}
