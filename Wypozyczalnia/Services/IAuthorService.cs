using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Services;

public interface IAuthorService
{
    IEnumerable<Author> GetAllAuthors();

    Author? GetAuthorById(int authorId);

    void InsertAuthor(Author author);

    void UpdateAuthor(Author author);

    void DeleteAuthor(int authorId);

    List<Author> GetAuthorsFromInput(string str);

    Task<JsonResult> SearchAuthor(string term);
}