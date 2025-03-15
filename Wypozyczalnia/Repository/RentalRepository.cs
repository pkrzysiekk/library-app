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
        return await _context.Rentals
            .FindAsync(rentalId);
    }

    public async Task InsertAsync(Rental rental)
    {
        await _context.Rentals.AddAsync(rental);
    }

    public void Update(Rental rental)
    {
        _context.Rentals.Update(rental);
    }

    public async Task DeleteAsync(int rentalId)
    {
        var rental = await GetByIdAsync(rentalId);
        if (rental != null)
        {
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