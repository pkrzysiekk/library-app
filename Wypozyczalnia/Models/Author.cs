using System.ComponentModel.DataAnnotations;

namespace Wypozyczalnia.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    public ICollection<Book> Books { get; set; } = [];
}