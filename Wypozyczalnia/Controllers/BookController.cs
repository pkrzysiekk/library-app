using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;
using Wypozyczalnia.Repository;

namespace Wypozyczalnia.Controllers;

public class BookController : Controller
{
    private IBookRepository _bookRepository;

    public BookController(LibraryContext context)
    {
        _bookRepository = new BookRepository(context);
    }

    public IActionResult Index()
    {
        var books = _bookRepository.GetAll();
        return View(books);
    }

    public async Task<IActionResult> Create()
    {
        // Pobieranie dostępnych autorów z repozytorium
        var authors = await _bookRepository.FetchAvaliableAuthors();

        var viewModel = new BookViewModel
        {
            // Przekazanie autorów do widoku w formie SelectListItem
            AvailableAuthors = authors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Pobieramy autorów na podstawie ID
            var authors = await _bookRepository.GetAuthorsByIdsAsync(model.SelectedAuthors);

            var book = new Book
            {
                Title = model.Title,
                Pages = model.Pages,
                IsBorrowed = model.IsBorrowed,
                Authors = (ICollection<Author>)authors // Powiązanie książki z autorami
            };

            await _bookRepository.InsertAsync(book);
            await _bookRepository.SaveAsync();
            return RedirectToAction("Index");
        }

        // W przypadku błędów w formularzu, ponownie ładujemy dostępnych autorów
        var authorsList = await _bookRepository.FetchAvaliableAuthors();
        model.AvailableAuthors = authorsList.Select(a => new SelectListItem
        {
            Value = a.Id.ToString(),
            Text = a.Name
        }).ToList();

        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View((book));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Book book)
    {
        if (ModelState.IsValid)
        {
            _bookRepository.UpdateAsync(book);
            await _bookRepository.SaveAsync();
            return RedirectToAction("Index");
        }
        return View(book);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _bookRepository.DeleteAsync(id);
        await _bookRepository.SaveAsync();
        return RedirectToAction("Index");
    }
}