using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Repository;

namespace Wypozyczalnia.Services;

public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;

    public RentalService(IRentalRepository rentalRepository)
    {
        _rentalRepository = rentalRepository;
    }

    public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
    {
        return await _rentalRepository.GetAll().ToListAsync();
    }

    public async Task<Rental?> GetRentalByIdAsync(int id)
    {
        return await _rentalRepository.GetByIdAsync(id);
    }

    public async Task CreateRentalAsync(RentalViewModel model)
    {
        var client = await _rentalRepository.GetClientByNameAsync(model.ClientName, model.ClientLastName);
        var book = await _rentalRepository.GetBookByTitleAsync(model.BookTitle);

        if (client == null || book == null || book.IsBorrowed)
        {
            return;
        }

        book.IsBorrowed = true;
        var rental = new Rental
        {
            BookId = book.Id,
            ClientId = client.Id,
            RentalDate = DateTime.Now,
            ExpectedReturnDate = model.ExpectedReturnDate,
            ActualReturnDate = model.ActualReturnDate,
            Charge = model.Charge
        };

        await _rentalRepository.InsertAsync(rental);
    }

    public async Task UpdateRentalAsync(RentalViewModel model)
    {
        var client = await _rentalRepository.GetClientByNameAsync(model.ClientName, model.ClientLastName);
        var book = await _rentalRepository.GetBookByTitleAsync(model.BookTitle);

        if (client == null || book == null)
        {
            return;
        }

        var rental = new Rental
        {
            Id = model.Id,
            BookId = book.Id,
            ClientId = client.Id,
            RentalDate = model.RentalDate,
            ExpectedReturnDate = model.ExpectedReturnDate,
            ActualReturnDate = model.ActualReturnDate,
            Charge = model.Charge
        };
        if (rental.ActualReturnDate != null)
        {
            book.IsBorrowed = false;
        }
        await _rentalRepository.UpdateAsync(rental);
    }

    public async Task DeleteRentalAsync(int id)
    {
        var rental = await _rentalRepository.GetByIdAsync(id);
        if (rental == null)
        {
            return;
        }
        rental.Book.IsBorrowed = false;
        await _rentalRepository.DeleteAsync(id);
    }
}