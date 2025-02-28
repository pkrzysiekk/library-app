namespace Wypozyczalnia.Models
{
    public class Author
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string LastName { get; set; }

        public ICollection<Position> Positions { get; set; }
        
    }
}
