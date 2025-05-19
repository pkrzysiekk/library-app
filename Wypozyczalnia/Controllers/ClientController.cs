using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Services;

[Authorize(Policy = "RequireElevatedPrivilleges")]
public class ClientController : Controller
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<IActionResult> Index()
    {
        var clients = await _clientService.GetAllClientsAsync();
        return View(clients);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _clientService.DeleteClientAsync(id);
        return RedirectToAction("Index");
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClientViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _clientService.CreateClientAsync(model);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        if (client == null) return NotFound();

        var model = ClientViewModel.ConvertToViewModel(client);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ClientViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _clientService.UpdateClientAsync(model);
            return RedirectToAction("Index");
        }
        return View(model);
    }
}