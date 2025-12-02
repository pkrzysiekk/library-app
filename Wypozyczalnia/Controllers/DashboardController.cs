using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Services;
using static System.Net.Mime.MediaTypeNames;

namespace Wypozyczalnia.Controllers;

[Authorize(Policy = "RequireElevatedPrivilleges")]
public class DashboardController : Controller
{
    private readonly IDashboardService _dashboardService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IAuthService _authService;

    public DashboardController
        (IDashboardService dashboardService,UserManager<IdentityUser> userManager,IAuthService authService)
    {
        _dashboardService = dashboardService;
        _userManager = userManager;
        _authService = authService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _dashboardService.GetAllStatistics();
        return View(model);
    }
    [HttpGet]
    public async Task <IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
           return RedirectToAction("Index");
        var model = new UserDashBoardViewModel()
        {
            User=user,
        };
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> GetStatistics(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user ==null)
            return NotFound();
        var userRoles = await _userManager.GetRolesAsync(user);
        var requiredRole = userRoles.FirstOrDefault(role => role == "TrialAdmin");
        bool isDemo =requiredRole == null;
        string statistics;
        if (isDemo)
            statistics = await _dashboardService.GetEncryptedStatistics();
        else
            statistics = await _dashboardService.GetStatisticsAsString();

        byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(statistics);

        return File(fileBytes, "text/plain", "statistics.txt");
    }
    [HttpPost]
    public async Task<IActionResult> Edit(UserDashBoardViewModel viewModel)
    {
        var user = await _userManager.FindByIdAsync(viewModel.User.Id);
        var userPassword = user.PasswordHash;
        var otp = _authService.GenerateOTP(userPassword);
        await _authService.AddOTP(viewModel.User.Id, otp);
        var model = new UserDashBoardViewModel()
        {
     
            Otp = otp
        };
        return View(model);
    }

}