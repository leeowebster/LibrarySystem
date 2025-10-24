
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibrarySystem.Infrastructure
{
    public class LibraryContext : DbContext, IDbContext
    {
        
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<Borrow> Borrows { get; set; }



    }
}
