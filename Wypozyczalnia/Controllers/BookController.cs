using Microsoft.AspNetCore.Mvc;
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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book)
    {
        if (ModelState.IsValid)
        {
            await _bookRepository.InsertAsync(book);
            await _bookRepository.SaveAsync();
            return RedirectToAction("Index");
        }
        return View(book);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
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