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

    public IQueryable<Book> GetAll()
    {
        return _context.Books
            .Include(b=>b.Authors);
    }

    public async Task<Book?> GetByIdAsync(int authorId)
    {
        var book = await _context.Books.FindAsync(authorId);
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

    public void UpdateAsync(Book book)
    {
        _context.Update(book);
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

    public async Task<IEnumerable<Author>> FetchAvaliableAuthors()
    {
        var authors = await _context.Authors.ToListAsync();
        return authors;
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

    async Task<IEnumerable<Author>> IBookRepository.GetAuthorsByIdsAsync(List<int> authorsId)
    {
        var authors = await _context.Authors.Where(a => authorsId.Contains(a.Id)).ToListAsync();
        return (ICollection<Author>)authors;
    }
}