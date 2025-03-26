using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Models;
using Wypozyczalnia.Repository;

namespace Wypozyczalnia.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task DeleteBookAsync(int bookId)
    {
        await _bookRepository.DeleteAsync(bookId);
        await _bookRepository.SaveAsync();
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAll().ToListAsync();
        return books;
    }

    public async Task<Book?> GetBookByIdAsync(int bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        return book;
    }

    public async Task InsertBookAsync(Book book)
    {
        await _bookRepository.InsertAsync(book);
        await _bookRepository.SaveAsync();
    }

    public async Task UpdateBookAsync(int id, Book book)
    {
        _bookRepository.Update(id, book);
        await _bookRepository.SaveAsync();
    }
}