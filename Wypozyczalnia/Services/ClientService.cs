using Wypozyczalnia.Models;
using Wypozyczalnia.Repository;

namespace Wypozyczalnia.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _clientRepository.GetAllAsync();
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        return await _clientRepository.GetByIdAsync(id);
    }

    public async Task CreateClientAsync(Client client)
    {
        if (client != null)
        {
            await _clientRepository.AddAsync(client);
        }
    }

    public async Task UpdateClientAsync(Client client)
    {
        if (client != null)
        {
            await _clientRepository.UpdateAsync(client);
        }
    }

    public async Task DeleteClientAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client != null)
        {
            await _clientRepository.DeleteAsync(client);
        }
    }
}