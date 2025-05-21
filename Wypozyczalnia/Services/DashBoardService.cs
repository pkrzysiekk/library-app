using Microsoft.AspNetCore.Identity;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Repository;

namespace Wypozyczalnia.Services;

public class DashBoardService : IDashboardService
{
    private IRentalRepository _rentalRepository;
    private IBookService _bookService;
    private UserManager<IdentityUser> _userManager;

    public DashBoardService(IRentalRepository rentalRepository, IBookService bookService, UserManager<IdentityUser> userManager)
    {
        _rentalRepository = rentalRepository;
        _bookService = bookService;
        _userManager = userManager;
    }

    public DashBoardViewModel GetAllStatistics()
    {
        int rentalCount = GetRentalCount();
        double sales = GetSales();
        int userCount = GetUserCount();
        int bookCount = GetBookCount();

        return new DashBoardViewModel()
        {
            RentalCount = rentalCount,
            RentalSales = sales,
            UserCount = userCount,
            BookCount = bookCount,
        };
    }

    public int GetRentalCount()
    {
        return _rentalRepository.GetAll().Count();
    }

    public double GetSales()
    {
        var sales = _rentalRepository.GetAll().Sum(x => x.Charge);
        return (double)sales;
    }

    public int GetUserCount()
    {
        return _userManager.Users.Count();
    }

    public int GetBookCount()
    {
        return _bookService.GetAllBooks().Count();
    }
}