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
        public DbSet<Position> Positions { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Position>().ToTable("Position");
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Rental>().ToTable("Rental");

        }

        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();
            if (context.Clients.Any())
            {
                return;
            }
            var wiedzmin = new Book { Title = "Wiedźmin", Pages = 550 };
            var sapkowski = new Author { Name = "Andrzej", LastName = "Sapkowski" };
            context.Books.Add(wiedzmin);
            context.Authors.Add(sapkowski);
            var position = new Position { Book = wiedzmin, Author = sapkowski };
            context.Positions.Add(position);

            var client = new Client { Name = "Krzysztof", LastName = "Pieczarka" };
            var rental = new Rental 
            {
            Book=wiedzmin,
            Client=client,
            RentalDate=DateTime.Now,
            ExpectedReturnDate = DateTime.Now.AddDays(14),
            ActualReturnDate = null,
            Charge = null
            };

            context.Clients.Add(client);
            context.Rentals.Add(rental);
            context.SaveChanges();



        }
    }
}