using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;
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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookViewModel model)
    {
        if (ModelState.IsValid)
        {
            var authors = _bookRepository.GetAuthorsFromInput(model.Authors);

            if (authors.Count == 0)
            {
                ModelState.AddModelError("Authors", "Authors are required");
                return View(model);
            }
            var book = new Book
            {
                Title = model.Title,
                Authors = authors,
                Pages = model.Pages,
            };
            book.Authors = authors;
            await _bookRepository.InsertAsync(book);
            await _bookRepository.SaveAsync();
            return RedirectToAction("Index");
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        BookViewModel model = new()
        {
            BookId = id,
            Title = book.Title,
            Pages = book.Pages,
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(BookViewModel model)
    {
        if (ModelState.IsValid)
        {
            var authors = _bookRepository.GetAuthorsFromInput(model.Authors);
            if (authors.Count == 0)
            {
                ModelState.AddModelError("Authors", "Authors are required");
                return View(model);
            }
            var book = new Book
            {
                Title = model.Title,
                Authors = authors,
                Pages = model.Pages,
            };
            _bookRepository.Update(model.BookId, book);
            await _bookRepository.SaveAsync();

            return RedirectToAction("Index");
        }
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _bookRepository.DeleteAsync(id);
        await _bookRepository.SaveAsync();
        return RedirectToAction("Index");
    }
}