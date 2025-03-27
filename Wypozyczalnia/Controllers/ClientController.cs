using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Models;
using Wypozyczalnia.Services;

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
    public async Task<IActionResult> Create([Bind("Name,LastName")] Client client)
    {
        if (ModelState.IsValid)
        {
            await _clientService.CreateClientAsync(client);
            return RedirectToAction("Index");
        }
        return View(client);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        if (client == null) return NotFound();
        return View(client);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Client client)
    {
        if (ModelState.IsValid)
        {
            await _clientService.UpdateClientAsync(client);
            return RedirectToAction("Index");
        }
        return View(client);
    }
}