using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Repository;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Controllers;

public class RentalController : Controller
{
    private readonly IRentalService _rentalService;
    private readonly IBookService _bookService;

    public RentalController(IRentalService rentalService, IBookService bookService)
    {
        _rentalService = rentalService;
        _bookService = bookService;
    }

    public async Task<IActionResult> Index()
    {
        var rentals = await _rentalService.GetAllRentalsAsync();
        return View(rentals);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RentalViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _rentalService.CreateRentalAsync(model);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var rental = await _rentalService.GetRentalByIdAsync(id);
        if (rental == null) return NotFound();

        var model = new RentalViewModel
        {
            Id = rental.Id,
            ClientName = rental.Client.Name,
            ClientLastName = rental.Client.LastName,
            BookTitle = rental.Book.Title,
            RentalDate = rental.RentalDate,
            ExpectedReturnDate = rental.ExpectedReturnDate,
            ActualReturnDate = rental.ActualReturnDate,
            Charge = rental.Charge
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(RentalViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _rentalService.UpdateRentalAsync(model);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _rentalService.DeleteRentalAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public JsonResult SearchBook(string term)
    {
        var books = _bookService.SearchBook(term);
        return Json(books);
    }
}