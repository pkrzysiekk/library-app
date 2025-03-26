using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Repository;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    public IActionResult Index()
    {
        var books = _bookService.GetAllBooksAsync().Result;
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
                bookImageLink = model.bookImageLink,
            };
            book.Authors = authors;
            await _bookService.InsertBookAsync(book);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpGet]
    public JsonResult SearchAuthor(string term)
    {
        return _bookService.SearchAuthor(term);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        BookViewModel model = new()
        {
            BookId = id,
            Title = book.Title,
            Pages = book.Pages,
            bookImageLink = book.bookImageLink
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
                bookImageLink = model.bookImageLink
            };
            await _bookService.UpdateBookAsync(model.BookId, book);

            return RedirectToAction("Index");
        }
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _bookService.DeleteBookAsync(id);
        return RedirectToAction("Index");
    }
}