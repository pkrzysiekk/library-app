using Wypozyczalnia.Models;

namespace Wypozyczalnia.Repository;

public interface IBookRepository
{
    IQueryable<Book> GetAllWithRelatedEntities();

    IQueryable<Book> GetAll();

    Task<Book?> GetByIdAsync(int authorId);

    Task InsertAsync(Book book);

    void Update(int id, Book book);

    Task DeleteAsync(int bookId);

    Task SaveAsync();
}