using Microsoft.AspNetCore.Identity;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Repository;

namespace Wypozyczalnia.Services;

public class DashBoardService : IDashboardService
{
    private IRentalRepository _rentalRepository;
    private IBookService _bookService;
    private UserManager<IdentityUser> _userManager;

    public DashBoardService(IRentalRepository rentalRepository,
        IBookService bookService,
        UserManager<IdentityUser> userManager)
    {
        _rentalRepository = rentalRepository;
        _bookService = bookService;
        _userManager = userManager;
    }

    public DashBoardViewModel GetAllStatistics()
    {
        return new DashBoardViewModel()
        {
            RentalCount = GetRentalCount(),
            RentalSales = GetSales(),
            UserCount = GetUserCount(),
            BookCount = GetBookCount(),
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