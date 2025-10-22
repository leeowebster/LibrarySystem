
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Domain.Interfaces
{
    public interface IDbContext 
    {
        DbSet<Books> Books { get; set; }
        DbSet<People> People { get; set; } 
        DbSet<Borrow> Borrows { get; set; }

        int SaveChanges();
    }
}
