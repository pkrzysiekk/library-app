﻿using Wypozyczalnia.Models;

namespace Wypozyczalnia.Repository;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();

    Task<Client?> GetByIdAsync(int id);

    Task AddAsync(Client client);

    Task UpdateAsync(Client client);

    Task DeleteAsync(Client client);
}