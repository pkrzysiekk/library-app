using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Repository;

public interface IBookRepository
{
    IQueryable<Book> GetAll();

    Task<Book?> GetByIdAsync(int authorId);

    Task InsertAsync(Book book);

    public List<Author> GetAuthorsFromInput(string str);

    void Update(int id, Book book);

    Task DeleteAsync(int bookId);

    JsonResult Search(string term);

    Task SaveAsync();
}