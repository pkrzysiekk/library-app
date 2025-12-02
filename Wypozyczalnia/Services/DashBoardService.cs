using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
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

    public async Task<DashBoardViewModel> GetAllStatistics()
    {
        var users = await GetUsers();
        return new DashBoardViewModel()
        {
            RentalCount = GetRentalCount(),
            RentalSales = GetSales(),
            UserCount = GetUserCount(),
            BookCount = GetBookCount(),
            users = users
        };
    }
    public async Task<string> GetStatisticsAsString()
    {
        var statistics = await GetAllStatistics();
        var sb = new StringBuilder();
        foreach(var user in statistics.users)
        {
            sb.AppendLine($"${user.UserName} ${user.Email}");
        }
        return sb.ToString();       
    }
    public async Task <string> GetEncryptedStatistics()
    {
        var statistics = await GetStatisticsAsString();
        var encrypted = EncryptString(statistics);
        return encrypted;
    }
    private string EncryptString(string toEncrypt)
    {
        int shift = 3;
        char[] buffer = toEncrypt.ToCharArray();

        for (int i = 0; i < buffer.Length; i++)
        {
            char c = buffer[i];

            if (char.IsLetter(c))
            {
                char a = char.IsUpper(c) ? 'A' : 'a';
                c = (char)(((c - a + shift) % 26) + a);
            }

            buffer[i] = c;
        }

        return new string(buffer);
    }
    private string DecryptString(string toDecrypt)
    {
        int shift = 3;
        char[] buffer = toDecrypt.ToCharArray();

        for (int i = 0; i < buffer.Length; i++)
        {
            char c = buffer[i];

            if (char.IsLetter(c))
            {
                char a = char.IsUpper(c) ? 'A' : 'a';
                c = (char)(((c - a - shift + 26) % 26) + a);
            }

            buffer[i] = c;
        }

        return new string(buffer);
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
    public async Task<IEnumerable<IdentityUser>> GetUsers()
    {
        var users =await _userManager.Users.ToListAsync();
        return users;

    }
}