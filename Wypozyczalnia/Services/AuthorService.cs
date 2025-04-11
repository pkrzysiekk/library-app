using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Repository;

namespace Wypozyczalnia.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public IEnumerable<Author> GetAllAuthors()
    {
        return _authorRepository.GetAll();
    }

    public Author? GetAuthorById(int authorId)
    {
        return _authorRepository.GetById(authorId);
    }

    public void InsertAuthor(AuthorViewModel model)
    {
        var author = model.ConvertToModel();
        _authorRepository.Insert(author);
        _authorRepository.Save();
    }

    public void UpdateAuthor(AuthorViewModel model)
    {
        var author = model.ConvertToModel();
        _authorRepository.Update(author);
        _authorRepository.Save();
    }

    public void DeleteAuthor(int authorId)
    {
        _authorRepository.Delete(authorId);
        _authorRepository.Save();
    }

    public List<Author> GetAuthorsFromInput(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return new List<Author>();
        }

        var authorNames = str.Split(',')
                             .Select(a => a.Trim())
                             .Where(a => !string.IsNullOrWhiteSpace(a))
                             .Distinct()
                             .ToList();

        List<Author> authorsList = new List<Author>();

        foreach (var author in authorNames)
        {
            string[] authorData = author.Split(' ');
            if (authorData.Length < 2) continue;

            string authorName = string.Join(" ", authorData.Take(authorData.Length - 1));

            string authorSurname = authorData.Last();

            var existingAuthor = _authorRepository.GetAll()
                .FirstOrDefault(a => a.Name == authorName && a.LastName == authorSurname);

            if (existingAuthor == null)
            {
                var newAuthor = new Author
                {
                    Name = authorName,
                    LastName = authorSurname
                };
                _authorRepository.Insert(newAuthor);
                authorsList.Add(newAuthor);
            }
            else
            {
                authorsList.Add(existingAuthor);
            }
        }

        return authorsList;
    }

    public async Task<JsonResult> SearchAuthor(string term)
    {
        var authors = await _authorRepository.GetAll()
                              .Where(a => a.Name.Contains(term))
                              .Take(10)
                              .ToListAsync();
        var json = new JsonResult(authors);
        return json;
    }
}