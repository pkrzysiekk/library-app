using Wypozyczalnia.Models.ViewModels;

namespace Wypozyczalnia.Services;

public interface IDashboardService
{
    public Task<DashBoardViewModel> GetAllStatistics();

    public int GetUserCount();

    public double GetSales();

    public int GetRentalCount();

    public int GetBookCount();
}