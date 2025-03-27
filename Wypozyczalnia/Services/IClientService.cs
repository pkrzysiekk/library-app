using Wypozyczalnia.Models;

namespace Wypozyczalnia.Services;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllClientsAsync();

    Task<Client?> GetClientByIdAsync(int id);

    Task CreateClientAsync(Client client);

    Task UpdateClientAsync(Client client);

    Task DeleteClientAsync(int id);
}