using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTOs
{
    internal class BookCreate
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime DatePublished { get; set; }

        public bool IsAvailable { get; set; } = true;

        public BookCreate(string Author, string Title, DateTime DatePublished) {
            this.Author = Author;
            this.Title = Title;
            this.DatePublished = DatePublished;
        }

    }
}
