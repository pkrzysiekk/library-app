using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;

namespace Wypozyczalnia.Repository;

public interface IRentalRepository
{
    IQueryable<Rental> GetAll();

    Task<Rental?> GetByIdAsync(int id);

    Task<Client?> GetClientByNameAsync(string name, string lastName);

    Task<Book?> GetBookByTitleAsync(string title);

    Task InsertAsync(Rental rental);

    Task UpdateAsync(Rental rental);

    Task DeleteAsync(int id);

    Task SaveAsync();
}