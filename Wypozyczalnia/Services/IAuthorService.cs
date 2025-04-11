using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;

namespace Wypozyczalnia.Services;

public interface IAuthorService
{
    IEnumerable<Author> GetAllAuthors();

    Author? GetAuthorById(int authorId);

    void InsertAuthor(AuthorViewModel model);

    void UpdateAuthor(AuthorViewModel model);

    void DeleteAuthor(int authorId);

    List<Author> GetAuthorsFromInput(string str);

    Task<JsonResult> SearchAuthor(string term);
}