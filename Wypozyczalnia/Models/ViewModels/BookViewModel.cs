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

    public Book ConvertToModel()
    {
        return new Book
        {
            Id = BookId,
            Title = Title,
            Pages = Pages,
            bookImageLink = bookImageLink
        };
    }

    public static BookViewModel ConvertToViewModel(Book book)
    {
        return new BookViewModel
        {
            BookId = book.Id,
            Title = book.Title,
            Pages = book.Pages,
            bookImageLink = book.bookImageLink
        };
    }
}