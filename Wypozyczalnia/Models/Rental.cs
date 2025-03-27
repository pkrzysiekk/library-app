using System.ComponentModel;

namespace Wypozyczalnia.Models;

public class Rental
{
    public int Id { get; set; }
    public int BookId { get; set; }

    public int ClientId { get; set; }

    [DisplayName("Rental Date")]
    public DateTime RentalDate { get; set; }

    [DisplayName("Expected Return Date")]
    public DateTime ExpectedReturnDate { get; set; }

    [DisplayName("Actual Return Date")]
    public DateTime? ActualReturnDate { get; set; }

    public Decimal? Charge { get; set; }

    public Book Book { get; set; }
    public Client Client { get; set; }
}