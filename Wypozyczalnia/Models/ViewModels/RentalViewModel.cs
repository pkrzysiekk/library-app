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
}