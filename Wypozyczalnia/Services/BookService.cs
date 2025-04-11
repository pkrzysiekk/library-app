using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Repository;

namespace Wypozyczalnia.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorService _authorService;

    public BookService(IBookRepository bookRepository, IAuthorService authorService)
    {
        _bookRepository = bookRepository;
        _authorService = authorService;
    }

    public async Task DeleteBookAsync(int bookId)
    {
        await _bookRepository.DeleteAsync(bookId);
        await _bookRepository.SaveAsync();
    }

    public IQueryable<Book> GetAllBooks()
    {
        var books = _bookRepository
            .GetAllWithRelatedEntities();
        return books;
    }

    public async Task<Book?> GetBookByIdAsync(int bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        return book;
    }

    private List<Author> ValidateAndGetAuthors(string authorsInput)
    {
        var authors = _authorService.GetAuthorsFromInput(authorsInput);
        if (authors.Count == 0)
        {
            throw new Exception("Authors are required");
        }
        return authors;
    }

    public async Task InsertBookAsync(BookViewModel model)
    {
        var authors = ValidateAndGetAuthors(model.Authors);
        var book = model.ConvertToModel();
        book.Authors = authors;

        await _bookRepository.InsertAsync(book);
        await _bookRepository.SaveAsync();
    }

    public async Task UpdateBookAsync(BookViewModel model)
    {
        var authors = _authorService.GetAuthorsFromInput(model.Authors);
        if (authors.Count == 0)
        {
            throw new Exception("Authors are required");
        }
        var book = model.ConvertToModel();
        book.Authors = authors;
        _bookRepository.Update(model.BookId, book);
        await _bookRepository.SaveAsync();
    }

    public List<Book>? SearchBook(string term)
    {
        var searchResult = _bookRepository.GetAll()
            .Where(b => b.Title.Contains(term) && !b.IsBorrowed)
            .Take(10)
            .ToList();
        return searchResult;
    }
}