using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Controllers;

public class DashboardController : Controller
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public IActionResult Index()
    {
        var model = _dashboardService.GetAllStatistics();
        return View(model);
    }
}