using Wypozyczalnia.Models.ViewModels;

namespace Wypozyczalnia.Services;

public interface IDashboardService
{
    public DashBoardViewModel GetAllStatistics();

    public int GetUserCount();

    public double GetSales();

    public int GetRentalCount();

    public int GetBookCount();
}