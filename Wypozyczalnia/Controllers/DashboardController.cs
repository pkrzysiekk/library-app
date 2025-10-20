using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Controllers;

[Authorize(Policy = "RequireElevatedPrivilleges")]
public class DashboardController : Controller
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _dashboardService.GetAllStatistics();
        return View(model);
    }
}