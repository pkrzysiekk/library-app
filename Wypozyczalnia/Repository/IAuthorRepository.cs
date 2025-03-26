using Wypozyczalnia.Models;

namespace Wypozyczalnia.Repository;

public interface IAuthorRepository
{
    IQueryable<Author> GetAll();

    Author? GetById(int authorId);

    void Insert(Author author);

    void Update(Author author);

    void Delete(int authorId);

    void Save();
}