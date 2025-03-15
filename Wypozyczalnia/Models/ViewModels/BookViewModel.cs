using System.ComponentModel.DataAnnotations;

namespace Wypozyczalnia.Models.ViewModels;

public class BookViewModel
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public int Pages { get; set; }
    public string Authors { get; set; }
}