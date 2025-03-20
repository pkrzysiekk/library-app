using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Wypozyczalnia.Models.ViewModels;

public class BookViewModel
{
   
    public int BookId { get; set; }
    [Display(Name = "Book's Title")]
    public string Title { get; set; }
    public int Pages { get; set; }
    [Display(Name = "Book's Image Url")]

    [Url(ErrorMessage = "Invalid Url!")]
    public string bookImageLink { get; set; }

    public string Authors { get; set; }
}