using System.ComponentModel.DataAnnotations;


namespace LibrarySystem.Domain.Models
{
    public class Borrow
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int PeopleID { get; set; }

        public DateTime DateBorrowed { get; set; }
        public DateTime? DateReturned { get; set; }

    }
}
