namespace Wypozyczalnia.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public  int BookId { get; set; }

        public int ClientId { get; set; }
        public DateTime RentalDate { get; set; }

        public DateTime ExpectedReturnDate { get; set; }

        public DateTime? ActualReturnDate { get; set; }

        public Decimal? Charge { get; set; }


        public Book Book { get; set; }
        public Client Client { get; set; }

    }
}