

namespace LibrarySystem.Domain.Models
{
    public class Books
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DatePublished { get; set; }
        public bool IsAvailable { get; set; }

    }
}
