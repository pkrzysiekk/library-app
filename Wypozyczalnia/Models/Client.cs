using System.Security.Cryptography.X509Certificates;

namespace Wypozyczalnia.Models
{
    public class Client
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}
