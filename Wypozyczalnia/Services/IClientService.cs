using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;

namespace Wypozyczalnia.Services;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllClientsAsync();

    Task<Client?> GetClientByIdAsync(int id);

    Task CreateClientAsync(ClientViewModel model);

    Task UpdateClientAsync(ClientViewModel model);

    Task DeleteClientAsync(int id);
}