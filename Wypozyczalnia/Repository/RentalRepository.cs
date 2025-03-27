using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;
using Wypozyczalnia.Repository;

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
            .Include(r => r.Client)
            .Include(r => r.Book)
            .AsQueryable();
    }

    public async Task<Rental?> GetByIdAsync(int id)
    {
        return await _context.Rentals
            .Include(r => r.Client)
            .Include(r => r.Book)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Client?> GetClientByNameAsync(string name, string lastName)
    {
        return await _context.Clients
            .FirstOrDefaultAsync(c => c.Name == name && c.LastName == lastName);
    }

    public async Task<Book?> GetBookByTitleAsync(string title)
    {
        return await _context.Books
            .FirstOrDefaultAsync(b => b.Title == title);
    }

    public async Task InsertAsync(Rental rental)
    {
        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Rental rental)
    {
        _context.Rentals.Update(rental);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var rental = await _context.Rentals.FindAsync(id);
        if (rental != null)
        {
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}