using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Services;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();

    Task<Book?> GetBookByIdAsync(int bookId);

    Task InsertBookAsync(Book book);

    Task UpdateBookAsync(int id, Book book);

    Task DeleteBookAsync(int bookId);

    List<Book?> SearchBook(string term);
}