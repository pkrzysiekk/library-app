using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;

namespace Wypozyczalnia.Services;

public interface IBookService
{
    IQueryable<Book> GetAllBooks();

    Task<Book?> GetBookByIdAsync(int bookId);

    Task InsertBookAsync(BookViewModel model);

    Task UpdateBookAsync(BookViewModel model);

    Task DeleteBookAsync(int bookId);

    List<Book>? SearchBook(string term);
}