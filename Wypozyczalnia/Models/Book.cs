using System.ComponentModel.DataAnnotations;

namespace Wypozyczalnia.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Pages { get; set; }

    [Display(Name = "Avaliability")]
    public bool IsBorrowed { get; set; } = false;

    [Url(ErrorMessage = "Invalid Url!")]
    [Display(Name = "Image")]

    public string bookImageLink { get; set; }

    public ICollection<Author> Authors { get; set; } = [];
}