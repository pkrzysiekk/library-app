using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Repository;

public class RentalRepository : IRentalRepository
{
    private readonly LibraryContext _context;

    public RentalRepository(LibraryContext context)
    {
        _context = context;
    }

    public IQueryable<Rental> GetAll()
    {
        return _context.Rentals
            .Include(r => r.Book)
            .Include(r => r.Client);
    }

    public async Task<Rental?> GetByIdAsync(int rentalId)
    {
        var rental = await _context.Rentals
            .Include(r => r.Book)
            .Include(r => r.Client)
            .FirstOrDefaultAsync(r => r.Id == rentalId);
        if (rental == null)
        {
            return null;
    }
        return rental;
    }

    public async Task InsertAsync(Rental rental)
    {
        await _context.Rentals.AddAsync(rental);
    }

    public async Task Update(int id, Rental rental)
    {
        var rentalToUpdate = await _context.Rentals
            .Include(r => r.Book)
            .Include(r => r.Client)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (rentalToUpdate != null)
    {
            rentalToUpdate.ExpectedReturnDate = rental.ExpectedReturnDate;
            rentalToUpdate.ActualReturnDate = rental.ActualReturnDate;
            rentalToUpdate.Charge = rental.Charge;
            rentalToUpdate.Book.IsBorrowed = rentalToUpdate.ActualReturnDate != null ? false : true;

            _context.Rentals.Update(rentalToUpdate);
        }
    }

    public async Task DeleteAsync(int rentalId)
    {
        var rental = await _context.Rentals.
            Include(r => r.Book)
            .FirstOrDefaultAsync(r => r.Id == rentalId);
        if (rental != null)
        {
            rental.Book.IsBorrowed = false;
            _context.Rentals.Remove(rental);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Client?> GetClientByNameAsync(string name, string lastName)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Name == name && c.LastName == lastName);
        return client;
    }

    public Task<Book?> GetBookByTitleAsync(string title)
    {
        var book = _context.Books.FirstOrDefaultAsync(b => b.Title == title);
        return book;
    }
}