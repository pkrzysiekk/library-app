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
            .Include(b => b.Authors);
    }

    public async Task<Book?> GetByIdAsync(int authorId)
    {
        var book = await _context.Books
            .FindAsync(authorId);
        if (book == null)
        {
            return null;
        }
        return book;
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

            string authorName = authorData[0];
            string authorSurname = string.Join(" ", authorData.Skip(1));

            var existingAuthor = _context.Authors
                .FirstOrDefault(a => a.Name == authorName && a.LastName == authorSurname);

            if (existingAuthor == null)
            {
                var newAuthor = new Author
                {
                    Name = authorName,
                    LastName = authorSurname
                };
                _context.Authors.Add(newAuthor);
                authorsList.Add(newAuthor);
            }
            else
            {
                authorsList.Add(existingAuthor);
            }
        }

        return authorsList;
    }

    public async Task InsertAsync(Book book)
    {
        await _context.Books.AddAsync(book);
    }

    public void Update(int id, Book book)
    {
        var existingBook = _context.Books
            .Include(b => b.Authors)
            .FirstOrDefault(b => b.Id == id);

        if (existingBook == null) return;

        existingBook.Title = book.Title;
        existingBook.Pages = book.Pages;

        var newAuthors = book.Authors.ToList();

        existingBook.Authors.Clear();
        _context.SaveChanges();
        foreach (var author in newAuthors)
        {
            existingBook.Authors.Add(author);
        }

        _context.SaveChanges();
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