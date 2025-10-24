using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTOs
{
    public class BookCreate
    {
       
        public string Author { get; set; } = "Unknown";
        public string Title { get; set; } = "Unknown";
        public DateTime DatePublished { get; set; } 
        public bool IsAvailable { get; set; } = true;

    }
}
