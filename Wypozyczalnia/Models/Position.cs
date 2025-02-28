namespace Wypozyczalnia.Models
{
    public class Position
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public  Book Book { get; set; }
        public  Author Author { get; set; }
    }
}
