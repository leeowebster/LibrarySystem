using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTOs
{
    public class BorrowDTO
    {
        public int Id { get; set; }
        [Required]
        public int? BookId { get; set; }

        [Required]
        public int? PersonId { get; set; }
    }
}
