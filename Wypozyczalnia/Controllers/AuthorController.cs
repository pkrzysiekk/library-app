using Microsoft.AspNetCore.Mvc;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;
using Wypozyczalnia.Repository;

namespace Wypozyczalnia.Controllers;

public class AuthorController : Controller
{
    private IAuthorRepository _authorRepository;

    public AuthorController(LibraryContext context)
    {
        _authorRepository = new AuthorRepository(context);
    }

    public IActionResult Index()
    {
        var authors = _authorRepository.GetAll()
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
            _authorRepository.Insert(author);
            _authorRepository.Save();
            return RedirectToAction("Index");
        }
        return View(author);
    }

    public IActionResult Edit(int id)
    {
        var author = _authorRepository.GetById(id);
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
            _authorRepository.Update(author);
            _authorRepository.Save();
            return RedirectToAction("Index");
        }
        return View(author);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        _authorRepository.Delete(id);
        _authorRepository.Save();
        return RedirectToAction("Index");
    }
}