namespace Wypozyczalnia.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int Pages { get; set; }

        public ICollection<Position> Positions { get; set; }
    }
}
