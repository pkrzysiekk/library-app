using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Wypozyczalnia.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Last Name is required")]

        public string LastName { get; set; }

        public ICollection<Rental>? Rentals { get; set; }
    }
}
