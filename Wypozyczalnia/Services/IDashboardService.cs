using Wypozyczalnia.Models.ViewModels;

namespace Wypozyczalnia.Services;

public interface IDashboardService
{
    public Task<DashBoardViewModel> GetAllStatistics();
    public Task<string> GetStatisticsAsString();
    public Task<string> GetEncryptedStatistics();

    public int GetUserCount();

    public double GetSales();

    public int GetRentalCount();

    public int GetBookCount();
}