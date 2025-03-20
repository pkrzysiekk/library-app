using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Repository;

public interface IRentalRepository
{
    IQueryable<Rental> GetAll();

    Task<Rental?> GetByIdAsync(int rentalId);

    Task InsertAsync(Rental rental);

    Task Update(int id, Rental rental);

    Task DeleteAsync(int rentalId);

    JsonResult SearchBook(string str);
    Task<Client?> GetClientByNameAsync(string name, string lastName);

    Task<Book?> GetBookByTitleAsync(string title);

    Task SaveAsync();
}