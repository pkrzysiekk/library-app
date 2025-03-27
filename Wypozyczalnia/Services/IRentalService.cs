using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Services;

public interface IRentalService
{
    Task<IEnumerable<Rental>> GetAllRentalsAsync();

    Task<Rental?> GetRentalByIdAsync(int id);

    Task CreateRentalAsync(RentalViewModel model);

    Task UpdateRentalAsync(RentalViewModel model);

    Task DeleteRentalAsync(int id);
}