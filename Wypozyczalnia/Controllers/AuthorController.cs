using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;
using Wypozyczalnia.Repository;
using Wypozyczalnia.Services;

namespace Wypozyczalnia.Controllers;

public class AuthorController : Controller
{
    private IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    public IActionResult Index()
    {
        var authors = _authorService.GetAllAuthors()
            .ToList();
        return View(authors);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Author author)
    {
        if (ModelState.IsValid)
        {
            _authorService.InsertAuthor(author);
            return RedirectToAction("Index");
        }
        return View(author);
    }

    public IActionResult Edit(int id)
    {
        var author = _authorService.GetAuthorById(id);
        if (author == null)
        {
            return NotFound();
        }
        return View(author);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Author author)
    {
        if (ModelState.IsValid)
        {
            _authorService.UpdateAuthor(author);
            return RedirectToAction("Index");
        }
        return View(author);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        _authorService.DeleteAuthor(id);
        return RedirectToAction("Index");
    }
}