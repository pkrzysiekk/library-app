using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;
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
    public IActionResult Create(AuthorViewModel model)
    {
        if (ModelState.IsValid)
        {
            _authorService.InsertAuthor(model);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    public IActionResult Edit(int id)
    {
        var author = _authorService.GetAuthorById(id);
        if (author == null)
        {
            return NotFound();
        }
        var model = AuthorViewModel.ConvertToViewModel(author);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AuthorViewModel model)
    {
        if (ModelState.IsValid)
        {
            _authorService.UpdateAuthor(model);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        _authorService.DeleteAuthor(id);
        return RedirectToAction("Index");
    }
}