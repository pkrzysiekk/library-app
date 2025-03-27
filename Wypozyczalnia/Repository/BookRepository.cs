using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Repository;

public class BookRepository : IBookRepository, IDisposable
{
    private bool _disposed;
    private readonly LibraryContext _context;

    public BookRepository(LibraryContext context)
    {
        _context = context;
    }

    public IQueryable<Book> GetAllWithRelatedEntities()
    {
        return _context.Books
            .Include(b => b.Authors);
    }

    public IQueryable<Book> GetAll()
    {
        return _context.Books;
    }

    public async Task<Book?> GetByIdAsync(int bookId)
    {
        var book = await _context.Books
            .FindAsync(bookId);
        if (book == null)
        {
            return null;
        }
        return book;
    }

    public async Task InsertAsync(Book book)
    {
        await _context.Books.AddAsync(book);
    }

    public void Update(int id, Book book)
    {
        var existingBook = GetAllWithRelatedEntities()
            .FirstOrDefault(b => b.Id == id);
        if (existingBook == null)
            return;
        existingBook.Title = book.Title;
        existingBook.Pages = book.Pages;
        existingBook.Authors = book.Authors;
        existingBook.bookImageLink = book.bookImageLink;
    }

    public async Task DeleteAsync(int bookId)
    {
        var bookToDelete = await _context.Books.FindAsync(bookId);
        if (bookToDelete != null)
        {
            _context.Books.Remove(bookToDelete);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}