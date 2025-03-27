using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;

    public BookController(IBookService bookService, IAuthorService authorService)
    {
        _bookService = bookService;
        _authorService = authorService;
    }

    public IActionResult Index()
    {
        var books = _bookService.GetAllBooks();
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
        if (!ModelState.IsValid)
            return View(model);
        try
        {
            await _bookService.InsertBookAsync(model);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
            return View(model);
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<JsonResult> SearchAuthor(string term)
    {
        return await _authorService.SearchAuthor(term);
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
        if (!ModelState.IsValid)
            return View(model);
        try
        {
            await _bookService.UpdateBookAsync(model);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
            return View(model);
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _bookService.DeleteBookAsync(id);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
        }
        return RedirectToAction("Index");
    }
}