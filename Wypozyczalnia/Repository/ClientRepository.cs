using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Repository;

public class ClientRepository : IClientRepository
{
    private readonly LibraryContext _context;

    public ClientRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task AddAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Client client)
    {
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }
}