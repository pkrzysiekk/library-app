using System.ComponentModel.DataAnnotations;

namespace Wypozyczalnia.Models;

public class Client
{
    public int Id { get; set; }

    public string Name { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    public ICollection<Rental>? Rentals { get; set; }
}