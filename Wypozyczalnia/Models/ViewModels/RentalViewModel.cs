using System.ComponentModel;

namespace Wypozyczalnia.Models.ViewModels;

public class RentalViewModel
{
    public int Id { get; set; }

    [DisplayName("Book Title")]
    public string BookTitle { get; set; }

    [DisplayName("Client Name")]
    public string ClientName { get; set; }

    [DisplayName("Client Last Name")]
    public string ClientLastName { get; set; }

    [DisplayName("Rental Date")]
    public DateTime RentalDate { get; set; }

    [DisplayName("Expected Return Date")]
    public DateTime ExpectedReturnDate { get; set; }

    [DisplayName("Actual Return Date")]
    public DateTime? ActualReturnDate { get; set; }

    [DisplayName("Charge")]
    public Decimal? Charge { get; set; }

    public Rental ConvertToModel()
    {
        return new Rental
        {
            Id = Id,
            RentalDate = RentalDate,
            ExpectedReturnDate = ExpectedReturnDate,
            ActualReturnDate = ActualReturnDate,
            Charge = Charge
        };
    }

    public static RentalViewModel ConvertToViewModel(Rental rental)
    {
        return new RentalViewModel
        {
            Id = rental.Id,
            BookTitle = rental.Book?.Title,
            ClientName = rental.Client?.Name,
            ClientLastName = rental.Client?.LastName,
            RentalDate = rental.RentalDate,
            ExpectedReturnDate = rental.ExpectedReturnDate,
            ActualReturnDate = rental.ActualReturnDate,
            Charge = rental.Charge
        };
    }
}