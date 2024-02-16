using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PublisherDomain;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cover> Covers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PublisherDB;")
                .LogTo(Console.WriteLine, new[] {DbLoggerCategory.Database.Command.Name}, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

    }

   /* protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        modelBuilder.Entity<Author>().HasData(
            new Author { AuthorId = 1, FirstName = "Aylan", LastName = "Naili" });

        var authors = new Author[]
        {
            new Author { AuthorId = 2, FirstName = "Jacque", LastName = "Sassi" },
            new Author { AuthorId = 3, FirstName = "Louis", LastName = "Pham" },
            new Author { AuthorId = 4, FirstName = "Pierre", LastName = "Madaci" },
            new Author { AuthorId = 5, FirstName = "Paul", LastName = "Pena" },
         };
        modelBuilder.Entity<Author>().HasData(authors);

        var books = new Book[]
        {
            new Book { BookId = 1, AuthorId = 1, Title = "Salut les copains", PublishDate = new DateOnly }
        }
    }*/
}
