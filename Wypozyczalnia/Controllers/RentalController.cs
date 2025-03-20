using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Repository;

namespace Wypozyczalnia.Controllers;

public class RentalController : Controller
{
    private IRentalRepository _rentalRepository;

    public RentalController(LibraryContext context)
    {
        _rentalRepository = new RentalRepository(context);
    }

    public IActionResult Index()
    {
        var rentals = _rentalRepository.GetAll()
            .ToList();
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
            var client = await _rentalRepository.GetClientByNameAsync(model.ClientName, model.ClientLastName);
            var book = await _rentalRepository.GetBookByTitleAsync(model.BookTitle);
            if (client == null || book == null)
            {
                ModelState.AddModelError("", "Client or book not found");
                return View(model);
            }
            if (book.IsBorrowed)
            {
                ModelState.AddModelError("", "Book is unavaliable");
                return View(model);
            }
            book.IsBorrowed = true;
            var rental = new Rental
            {
                BookId = book.Id,
                ClientId = client.Id,
                RentalDate = DateTime.Now,
                ExpectedReturnDate = model.ExpectedReturnDate,
                ActualReturnDate = model.ActualReturnDate,
                Charge = model.Charge
            };
            await _rentalRepository.InsertAsync(rental);
            await _rentalRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        // Pobranie istniejącego wypożyczenia na podstawie ID
        var rental = await _rentalRepository.GetByIdAsync(id);
        if (rental == null)
        {
            return NotFound();
        }

        // Konwersja do ViewModel
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
            var Client = await _rentalRepository.GetClientByNameAsync(model.ClientName, model.ClientLastName);
            var Book = await _rentalRepository.GetBookByTitleAsync(model.BookTitle);
            if (Client == null || Book == null)
            {
                ModelState.AddModelError("", "Client or book not found");
                return View(model);
            }
            var rental = new Rental
            {
                Id = model.Id,
                BookId = Book.Id,
                ClientId = Client.Id,
                RentalDate = model.RentalDate,
                ExpectedReturnDate = model.ExpectedReturnDate,
                ActualReturnDate = model.ActualReturnDate,
                Charge = model.Charge
            };
            await _rentalRepository.Update(model.Id, rental);
            await _rentalRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _rentalRepository.DeleteAsync(id);
        await _rentalRepository.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public JsonResult SearchBook(string term)
    {
        var book = _rentalRepository.SearchBook(term);
        return book;
    }
}