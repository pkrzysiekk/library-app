using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Rental>().ToTable("Rental");
            modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity(j => j.ToTable("Positions"));
        }

        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();
            if (context.Clients.Any())
            {
                return;
            }
            List<Author> authors = new List<Author>
        {
            new Author {  Name = "Stephen", LastName = "King" },
            new Author { Name = "J.K.", LastName = "Rowling" },
            new Author {Name ="Thomas H.", LastName="Cormen" },
            new Author {Name ="Charles E. ", LastName="Leiserson" },
            new Author {Name ="Ronald L.", LastName="Rivest" },
            new Author {Name ="Clifford ", LastName="Stein" },
        };

            // Lista książek
            List<Book> books = new List<Book>
        {
            new Book
            {
                Title = "The Shining",
                Pages = 447,
                IsBorrowed = false,
                bookImageLink = "https://prodimage.images-bn.com/pimages/9780345806789_p0_v8_s1200x630.jpg",
                Authors = new List<Author> { authors[0] }
            },
            new Book
            {
                Title = "Harry Potter and the Sorcerer's Stone",
                Pages = 309,
                IsBorrowed = true,
                bookImageLink = "https://m.media-amazon.com/images/I/71XqqKTZz7L._AC_UF1000,1000_QL80_.jpg",
                Authors = new List<Author> {authors[1] }
            },
            new Book
            {
                Title = "Introduction to Algorithms, fourth edition",
                Pages = 1312,
                IsBorrowed = false,
                bookImageLink = "https://m.media-amazon.com/images/I/61Mw06x2XcL.jpg",
                Authors = new List<Author> { authors[2], authors[3], authors[4], authors[5] }
            }
        };

            // Lista klientów
            List<Client> clients = new List<Client>
        {
            new Client { Name = "John", LastName = "Doe" },
            new Client {  Name = "Jane", LastName = "Smith" }
        };

            // Lista wypożyczeń
            List<Rental> rentals = new List<Rental>
        {
            new Rental
            {
                BookId = 2,
                ClientId = 1,
                RentalDate = DateTime.Now.AddDays(-7),
                ExpectedReturnDate = DateTime.Now.AddDays(7),
                ActualReturnDate = null,
                Charge = null,
                Book = books[1],
                Client = clients[0]
            }
        };
            context.AddRange(authors);
            context.AddRange(rentals);
            context.AddRange(clients);
            context.AddRange(books);
            context.SaveChanges();
        }
    }
}