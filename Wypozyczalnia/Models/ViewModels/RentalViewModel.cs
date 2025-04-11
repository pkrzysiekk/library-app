namespace Wypozyczalnia.Models.ViewModels;

public class RentalViewModel
{
    public int Id { get; set; }

    public string BookTitle { get; set; }
    public string ClientName { get; set; }
    public string ClientLastName { get; set; }
    public DateTime RentalDate { get; set; }

    public DateTime ExpectedReturnDate { get; set; }

    public DateTime? ActualReturnDate { get; set; }

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