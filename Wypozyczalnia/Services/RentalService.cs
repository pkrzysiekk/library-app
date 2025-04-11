using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
        var rental = await GetValidatedRental(model);
        if (rental == null)
        {
            return;
        }
        rental.Book.IsBorrowed = true;
        rental.RentalDate = DateTime.Now;
        await _rentalRepository.InsertAsync(rental);
    }

    public async Task UpdateRentalAsync(RentalViewModel model)
    {
        var rental = await GetValidatedRental(model);

        if (rental == null)
        {
            return;
        }
        if (rental.ActualReturnDate != null)
        {
            rental.Book.IsBorrowed = false;
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

    private async Task<Rental?> GetValidatedRental(RentalViewModel model)
    {
        var client = await _rentalRepository.GetClientByNameAsync(model.ClientName, model.ClientLastName);
        var book = await _rentalRepository.GetBookByTitleAsync(model.BookTitle);

        if (client == null || book == null)
        {
            return null;
        }

        var rental = model.ConvertToModel();
        rental.Client = client;
        rental.Book = book;
        return rental;
    }
}