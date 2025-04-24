using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Controllers;

public class RentalController : Controller
{
    private readonly IRentalService _rentalService;
    private readonly IBookService _bookService;
    private readonly IValidator<RentalViewModel> _validator;

    public RentalController(IRentalService rentalService,
        IBookService bookService,
        IValidator<RentalViewModel> validator)
    {
        _rentalService = rentalService;
        _bookService = bookService;
        _validator = validator;
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
        var validationResult = await _validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View(model);
        }

        await _rentalService.CreateRentalAsync(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var rental = await _rentalService.GetRentalByIdAsync(id);
        if (rental == null) return NotFound();

        var model = RentalViewModel.ConvertToViewModel(rental);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(RentalViewModel model)
    {
        var validationResult = await _validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View(model);
        }
        return RedirectToAction(nameof(Index));
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