using Wypozyczalnia.Models;

namespace Wypozyczalnia.Repository;

public interface IBookRepository
{
    IQueryable<Book> GetAll();

    Task<Book?> GetByIdAsync(int authorId);

    Task InsertAsync(Book book);

    Task<IEnumerable<Author>> FetchAvaliableAuthors();

    void UpdateAsync(Book book);

    Task DeleteAsync(int bookId);

    Task<IEnumerable<Author>> GetAuthorsByIdsAsync(List<int> authorsId);

    Task SaveAsync();
}